import React from 'react';

import defaultTo from 'lodash/defaultTo';

const traits = {
    smz3: {
        id: 'smz3',
        smz3: true,
        z3: true,
        sm: true,
        mapping: 'exhirom'
    },
    sm: {
        id: 'sm',
        sm: true,
        mapping: 'lorom'
    },
    none: {
        id: 'none'
    }
};

export function resolveGameTraits(gameId) {
    return defaultTo(traits[gameId], traits.none);
}

export const GameTraitsCtx = React.createContext(traits.none);
