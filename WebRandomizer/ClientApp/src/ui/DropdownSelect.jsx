import React, { useState } from 'react';
import styled, { createGlobalStyle } from 'styled-components';
import { Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

import castArray from 'lodash/castArray';

// through bootstrap "$input-btn-border-width" -> "$border-width"
const inputBorderWidth = '1px';
const gray600 = '#6c757d';
const caretWidth = '.3em';
// through bootstrap "$caret-width" * .85
const caretSpacing = `${.3 * .85}em`;

/* Even though binding styling to components is good for all the reasons
   styled-components points out, we can't do that easily when integrating with
   bootstrap since styled-components has a bad time with innerRef. Because of
   this it is easier to adjust the style with global css.
*/
const InputGroupDropdownStyling = createGlobalStyle`
  .input-group {
    > .dropdown {
      flex: 1 1 0%;

      > .form-control {
        margin-bottom: 0;
        display: flex;
        align-items: center;
        justify-content: space-between;
      }

      :not(:last-child) > .form-control {
        border-top-right-radius: 0;
        border-bottom-right-radius: 0;
      }
      :not(:first-child) > .form-control {
        border-top-left-radius: 0;
        border-bottom-left-radius: 0;
      }

      + .dropdown > .form-control,
      + .form-control,
      + .form-select,
      + .form-file {
        margin-left: -${inputBorderWidth};
      }
    }

    > .form-control,
    > .form-select,
    > .form-file {
      + .dropdown > .form-control {
        margin-left: -${inputBorderWidth};
      }
    }
  }
`;

const Placeholder = styled.span`
  color: ${gray600};
  white-space: nowrap;
`;

/* The caret needs to be replicated since bootstrap applies it as an pseudo
   after element to the dropdown button, while we need it after an arbitrary
   option element.
*/
const Caret = styled.div`
  margin-left: ${caretSpacing};
  border: ${caretWidth} solid;
  border-right-color: transparent;
  border-left-color: transparent;
  border-${props => props.up ? 'top' : 'bottom'}: none;
`;

const menuModifiers = {
    adjustStyle: {
        enabled: true, order: 875,
        fn: (data) => ({
            ...data, styles: {
                ...data.styles,
                marginTop: 0,
                borderTop: 'none',
                borderTopLeftRadius: 0,
                borderTopRightRadius: 0,
                width: '100%',
                maxHeight: '25em',
                overflow: 'hidden auto',
            }
        })
    }
};

export default function DropdownSelect(props) {
    const [open, setOpen] = useState(false);

    const { index, onIndexChange } = props;
    const children = castArray(props.children);

    const toggle = () => setOpen(!open);
    const select = (i) => { onIndexChange && onIndexChange(i); };

    return (
        <>
            <InputGroupDropdownStyling />
            <Dropdown isOpen={open} toggle={toggle}>
                <DropdownToggle
                    tag="div"
                    className="form-control"
                    data-toggle="dropdown"
                    aria-expanded={open}>
                    {!open && index != null ?
                        children[index] :
                        <Placeholder>{props.placeholder}</Placeholder>
                    }
                    <Caret up={open} />
                </DropdownToggle>
                <DropdownMenu modifiers={menuModifiers}>
                    {children.map((option, i) =>
                        <DropdownItem key={i}
                            active={i === index}
                            onClick={() => select(i)}>
                            {option}
                        </DropdownItem>
                    )}
                </DropdownMenu>
            </Dropdown>
        </>
    );
}
