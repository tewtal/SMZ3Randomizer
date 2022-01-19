import styled from 'styled-components';
import { CardHeader, Label, Button } from 'reactstrap';
import Markdown from '../ui/Markdown';

/* Implement "layered" card headers by increase the amount of black "shade" */
export const LevelCardHeader = styled(CardHeader)`
  background-color: rgba(0,0,0,${({ level }) => .03 * level});
`;

/* Use a custom "bullet point" when displaying hierarchical logic */
export const LogicMarkdown = styled(Markdown)`
  ul {
    list-style: none;
    padding: 0;

    ul {
      li {
        position: relative;
        padding-left: 1em;
      }

      li:before {
        content: "";
        display: inline-block;
        position: absolute;
        top: .25em;
        left: 0;
        height: 1em;
        width: 1em;
        background-image: url(${process.env.PUBLIC_URL}/ui/logic_list_item.svg);
      }
    }
  }
`;

/* Skipped active, active+focus. color-yiq is bootstrap's contrast picker
 * between dark ($gray-900), and light ($white) */
export const ToggleButton = styled(Button)`
  &.btn-light {
    color: #212529;               /* color: color-yiq(background) => $gray-900 */
    background-color: #E9ECEF;    /* background: $input-group-addon-bg */
    border-color: #CED4DA;        /* border: $input-group-addon-border-color */
    &:hover {
      color: #212529;             /* hover-color: color-yip(hover-background) => $gray-900 */
      background-color: #D2D8DE;  /* hover-background: darken(background, 7.5%) */
      border-color: #B1BBC4;      /* hover-border: darken(border, 10%) */
    }
    &:focus, &.focus {
      color: #212529;             /* hover-color */
      background-color: #D2D8DE;  /* hover-background */
      border-color: #B1BBC4;      /* hover-border */
      box-shadow: 0 0 0 .2rem /* $btn-focus-width */ rgba(180, 186, 191, .5); /* mix(color, border, 15%), .5 */
    }
  }
`;

export const IntroLabel = styled(Label)`
  display: flex;
  align-items: center;
  & > .icon {
    width: 1.25em;
    height: 1.25em;
  }
`;
