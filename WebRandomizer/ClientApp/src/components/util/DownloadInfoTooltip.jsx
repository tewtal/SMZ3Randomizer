import React from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { UncontrolledTooltip } from 'reactstrap';
import Markdown from '../Markdown';

import { InfoCircleFill } from './BootstrapIcon';

const smz3Tooltip = `
Filename parts legend:

* \`SMZ3\`: Super Metroid / Link to the Past Randomizer
* \`V\`: Major.Minor version
* \`ZL+SL\`: LttP and SM logic
  * LttP: Normal, \`n\`
  * SM: Normal, \`n\`, or Hard, \`h\`
* \`S\`: Sword location
  * Randomized if missing
  * On Link's Uncle if \`u\`
  * Early location if \`e\`
* \`M\`: Morph location
  * Randomized if missing
  * Original location if \`o\`
  * Early location if \`e\`
* Seed number, or seed guid for race roms
* Player name for Multiworld roms
`;

const smTooltip = `
Filename parts legend:

* \`SM\`: Super Metroid Randomizer
* \`V\`: Major.Minor version
* \`L\`: Logic
  * Casual if \`c\`
  * Tournament if \`t\`
* \`I\`: Item placement
  * Major/Minor split if \`s\`
  * Full randomization if \`f\`
* Seed number, or seed guid for race roms
* Player name for Multiworld roms
`;

const InfoHover = styled(InfoCircleFill)`
  width: 1em;
  height: 1em;
`;

const tooltipWidth = '350px';

const tooltip_modifiers = {
    adjustStyle: {
        enabled: true, order: 875,
        fn: (data) => ({ ...data, styles: { ...data.styles, maxWidth: tooltipWidth } })
    }
};

const TooltipStyling = createGlobalStyle`
  .rom-info .tooltip-inner {
    max-width: ${tooltipWidth}
  }
`;

const StyledMarkdown = styled(Markdown)`
  text-align: left;
  color: silver;

  code {
    font-size: 100%;
    color: white;
  }

  ul {
    list-style: none;
    padding: 0;

    ul {
      padding-left: 2em;
    }
  }
`;

export default function DownloadInfoTooltip({ gameId, ...props }) {
    return (
        <div {...props}>
            <InfoHover id="download-tooltip" className="text-primary" />
            <TooltipStyling/>
            <UncontrolledTooltip className="rom-info" modifiers={tooltip_modifiers} placement="right" target="download-tooltip">
                <StyledMarkdown text={{ smz3: smz3Tooltip, sm: smTooltip }[gameId]} />
            </UncontrolledTooltip>
        </div>
    );
}
