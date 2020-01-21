import React from 'react';
import SvgIcon from '../components/util/SvgIcon';

export default function PlusIcon() {
    return (
        <SvgIcon>
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 8 8">
                <path fill="white" d="M 3 0 v 3 h -3 v 2 h 3 v 3 h 2 v -3 h 3 v -2 h -3 v -3 h -2 z" />
            </svg>
        </SvgIcon>
    );
}
