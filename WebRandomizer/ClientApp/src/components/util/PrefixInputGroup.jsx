import React from 'react';
import { InputGroup, InputGroupAddon, InputGroupText } from 'reactstrap';

export default function PrefixedIndexGroup(props) {
    return <InputGroup className={props.className}>
        <InputGroupAddon addonType="prepend">
            <InputGroupText>{props.prefix}</InputGroupText>
        </InputGroupAddon>
        {props.children}
    </InputGroup>;
}
