import React from 'react';
import styled from 'styled-components';

// through bootstrap "$primary" -> "$blue"
const primary = '#007bff';
// through bootstrap "$border-radius"
const borderRadius = '.25rem';

const StyledIcon = styled.div`
  width: 1em;
  height: 1em;
  display: inline-flex;
  justify-content: center;
  align-items: center;
  border: ${primary} solid 1px;
  border-radius: ${borderRadius};
  background-color: ${primary};

  & > * {
    width: .5em;
    height: .5em;
  }
`;

export default function SvgIcon({ children }) {
    return (
        <StyledIcon>
            {children}
        </StyledIcon>
    );
}
