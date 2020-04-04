import React from 'react';
import styled from 'styled-components';

// through bootstrap "$primary" -> "$blue"
const primary = '#007bff';

const StyledIcon = styled.div`
  display: inline-flex;
  justify-content: center;
  align-items: center;
  color: ${primary};

  & > * {
    width: 1em;
    height: 1em;
  }
`;

export default function BootstrapIcon({ id, children }) {
    return (
        <StyledIcon id={id}>
            {children}
        </StyledIcon>
    );
}
