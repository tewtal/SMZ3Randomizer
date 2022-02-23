import React from 'react';

/* based on https://icons.getbootstrap.com/icons/hourglass-split/ */
export const Hourglass = (props) => (
    <svg {...props} viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg">
        <path fill="var(--sand)" d="m11.5 4.5-3 3.15v3.35h3v3h-7v-3h3v-3.35l-3-3.15" />
        <path fill="none" stroke="var(--glass)" d="m12 2v1c0 1.5-0.9 2.9-2.1 3.5-0.6 0.3-0.9 0.7-0.9 1.15v0.7c0 0.45 0.3 0.85 0.9 1.15 1.2 0.6 2.1 2 2.1 3.5v1m-8-12v1c0 1.5 0.9 2.9 2.1 3.5 0.6 0.3 0.9 0.7 0.9 1.15v0.7c0 0.45-0.3 0.85-0.9 1.15-1.2 0.6-2.1 2-2.1 3.5v1" />
        <path fill="none" stroke="var(--frame)" strokeLinecap="round" d="m3.5 14.5h9m-9-13h9" />
    </svg>
);

/* based on https://icons.getbootstrap.com/icons/exclamation-triangle-fill/ */
export const WarningTriangle = (props) => (
    <svg {...props} viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg">
        <path fill="var(--background)" stroke="var(--outline)" d="m15.4 14.2c-0.2 0.346-0.551 0.314-0.551 0.314h-13.7s-0.351 0.0327-0.551-0.314c-0.2-0.346 0.0036-0.634 0.0036-0.634l6.86-11.7s0.147-0.32 0.547-0.32 0.547 0.32 0.547 0.32l6.86 11.7s0.204 0.287 0.0036 0.634z" />
        <path fill="var(--outline)" d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z" />
    </svg>
);
