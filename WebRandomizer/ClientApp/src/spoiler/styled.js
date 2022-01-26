import { NavLink } from 'reactstrap';
import styled from 'styled-components';

import { Search, JournalArrowDown } from '../ui/BootstrapIcon';

export const SmallNavLink = styled(NavLink)`
  font-size: .87em;
  font-weight: bold;
  padding-top: 6px;
  padding-bottom: 6px;
  padding-right: 9px;
  padding-left: 9px;
`;

export const StyledTable = styled.table.attrs({
    className: "table table-sm table-borderless"
})`
  > tbody > tr {
    border-bottom: 1px solid #E0E0E0;
    > td:first-child { width: 60% }
  }
`;

export const SearchIcon = styled(Search)`
  width: 1em;
  height: 1em;
`;

export const DownloadIcon = styled(JournalArrowDown)`
  width: 1em;
  height: 1em;
`;
