import styled from 'styled-components';

import { CheckSquareFill, ChatLeftDots, ArrowClockwise } from '../ui/BootstrapIcon';
import { Hourglass, WarningTriangle } from '../ui/ColoredIcon';

export const LogMessageRow = styled.div.attrs({
    className: "d-flex align-items-center"
})`
    pointer-events: none;
    &:hover { background: #ffeeee; }

    div > a { pointer-events: auto; }
    &:hover div > a { text-decoration: none; }
`;

const LogIcon = styled.div`
  width: 1.5em;
  height: 1.5em;
`;

export const Ok = styled(LogIcon).attrs({ as: CheckSquareFill })`
  color: green;
`;

export const Delay = styled(LogIcon).attrs({ as: Hourglass })`
  --frame: saddlebrown;
  --glass: lightblue;
  --sand: gold;
`;

export const Msg = styled(LogIcon).attrs({ as: ChatLeftDots })`
  color: black;
`;

export const Issue = styled(LogIcon).attrs({ as: WarningTriangle })`
  --background: gold;
  --outline: black;
`;

export const Reload = styled(LogIcon).attrs({ as: ArrowClockwise })`
  color: red;
`;
