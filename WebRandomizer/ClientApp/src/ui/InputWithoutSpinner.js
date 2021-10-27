import styled from 'styled-components';
import { Input } from 'reactstrap';

export default styled(Input)`
  /* For Firefox */
  appearance: textfield;
  /* For Chromium */
  &::-webkit-inner-spin-button,
  &::-webkit-outer-spin-button {
    appearance: none;
  }
`;
