import React from 'react';
import { Card, CardHeader, CardBody } from 'reactstrap';

import classNames from 'classnames';

export default function MessageCard({ error, title, msg }) {
    return (
        <Card>
            <CardHeader className={classNames({
                    'bg-danger': error,
                    'bg-primary': !error
                }, 'text-white'
            )}>
                {title}
            </CardHeader>
            <CardBody>
                {msg}
            </CardBody>
        </Card>
    );
}
