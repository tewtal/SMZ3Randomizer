import { createGlobalStyle } from 'styled-components';

export default createGlobalStyle`
  html {
    font-size: 14px;
    @media(min-width: 768px) {
      font-size: 16px;
    }
  }
`;
