import React from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { UncontrolledTooltip } from 'reactstrap';
import BootstrapIcon from './BootstrapIcon';
import Markdown from '../Markdown';

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
            <InfoIcon id="download-tooltip" />
            <TooltipStyling/>
            <UncontrolledTooltip className="rom-info" modifiers={tooltip_modifiers} placement="right" target="download-tooltip">
                <StyledMarkdown text={{ smz3: smz3Tooltip, sm: smTooltip }[gameId]} />
            </UncontrolledTooltip>
        </div>
    );
}

/* https://github.com/twbs/icons/blob/v1.0.0-alpha3/icons/info-circle-fill.svg */
function InfoIcon({ id }) {
    return (
        <BootstrapIcon id={id}>
            <svg viewBox="0 0 16 16" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                <path fillRule="evenodd" clipRule="evenodd" d="M8 16A8 8 0 108 0a8 8 0 000 16zm.93-9.412l-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533L8.93 6.588zM8 5.5a1 1 0 100-2 1 1 0 000 2z" />
            </svg>
        </BootstrapIcon>
    );
}
