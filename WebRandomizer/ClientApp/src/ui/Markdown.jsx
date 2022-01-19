import React, { useState, useEffect } from 'react';
import { createGlobalStyle } from 'styled-components';
import ReactMarkdown from 'react-markdown';

import classNames from 'classnames';

import attempt from 'lodash/attempt';

const GlobalMarkdownStyle = createGlobalStyle`
  .markdown {
    h1 { font-size: 2rem; }
    h2 { font-size: 1.75rem; }
    h3 { font-size: 1.5rem; }
    h4 { font-size: 1.25rem }
    h5 { font-size: 1rem; }
    h6 { font-size: 1rem; font-style: italic; }
    h1, h2, h3 { margin-top: 3rem; }
    h4 { margin-top: 1.5rem; }
    h5 { margin-top: 1rem; }

    ul, ol {
      p + ul, p + ol {
        margin-bottom: 1rem;
      }
    }
  }
`;

export default function Markdown(props) {
    const { className, text = '' } = props;
    const [sourceText, setSourceText] = useState('');

    useEffect(() => {
        if (props.source) {
            attempt(async () => {
                try {
                    const response = await fetch(props.source);
                    setSourceText(await response.text());
                } catch (error) {
                    setSourceText(`Could not load text because: ${error}`);
                }
            });
        }
    }, [props.source]);

    return (
        <>
            <GlobalMarkdownStyle/>
            <ReactMarkdown
                className={classNames('markdown', className)}
                source={sourceText || text}
            />
        </>
    );
}
