import React, { useState, useEffect } from 'react';
import ReactMarkdown from 'react-markdown';

import attempt from 'lodash/attempt';

export default function Markdown(props) {
    const [text, setText] = useState('');

    useEffect(() => {
        attempt(async () => {
            try {
                const response = await fetch(props.source);
                setText(await response.text());
            } catch (error) {
                setText(`Could not load text because: ${error}`);
            }
        });
    }, [props.source]);

    return <ReactMarkdown source={text} />;
}
