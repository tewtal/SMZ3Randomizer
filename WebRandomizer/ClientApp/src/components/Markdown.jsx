import React, { useState, useEffect } from 'react';
import ReactMarkdown from 'react-markdown';

import attempt from 'lodash/attempt';

export function Markdown(props) {
    const [helpText, setHelpText] = useState('');

    useEffect(() => {
        attempt(async () => {
            try {
                const response = await fetch(props.mdLink);
                setHelpText(await response.text());
            } catch (error) {
                setHelpText(`Could not load help text because: ${error}`);
            }
        });
    }, [props.mdLink]);

    return <ReactMarkdown source={helpText} />;
}
