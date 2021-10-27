import React, { useState } from 'react';
import { isElement, isFragment } from 'react-is';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { Container, Navbar, NavbarBrand, NavbarToggler, Collapse, Nav, NavItem, NavLink } from 'reactstrap';
import { UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

const StyledNavbar = styled(Navbar)`
  box-shadow: 0 .25rem .75rem rgba(0, 0, 0, .05);
`;

const StyledNavbarBrand = styled(NavbarBrand)`
  white-space: normal;
  text-align: center;
  word-break: break-all;
`;

export function NavMenuDropdown({ to, children }) {}
export function NavMenuItem({ title, children }) {}

export function NavMenu({ brand, nav, dropdown }) {
    const [showMenu, setShowMenu] = useState(false);

    return (
        <header>
            <StyledNavbar className="border-bottom mb-3" expand="sm" color="white" light>
                <Container>
                    {isElementOfType(brand, NavMenuItem) && createBrand(brand)}
                    <NavbarToggler className="mr-2" onClick={() => setShowMenu(!showMenu)} />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" navbar isOpen={showMenu}>
                        <Nav className="flex-grow" navbar>
                            {castElementArray(nav).map((item, i) => isElementOfType(item, NavMenuItem) && (
                                <NavItem key={i}>{createNavLink(item)}</NavItem>
                            ))}
                            {isElementOfType(dropdown, NavMenuDropdown) && (
                                <UncontrolledDropdown nav inNavbar>
                                    <DropdownToggle className="text-dark" nav caret>{dropdown.props.title}</DropdownToggle>
                                    <DropdownMenu>
                                        {dropdown.props.children.map((item, i) => isElementOfType(item, NavMenuItem) && (
                                            <DropdownItem key={i}>{createNavLink(item)}</DropdownItem>
                                        ))}
                                    </DropdownMenu>
                                </UncontrolledDropdown>
                            )}
                        </Nav>
                    </Collapse>
                </Container>
            </StyledNavbar>
        </header>
    );

    function createBrand(item) {
        return (
            <StyledNavbarBrand tag={Link} to={item.props.to}>
                {item.props.children}
            </StyledNavbarBrand>
        );
    }

    function createNavLink(item) {
        return (
            <NavLink className="text-dark" tag={Link} to={item.props.to}>
                {item.props.children}
            </NavLink>
        );
    }
}

function castElementArray(object) {
    return isFragment(object) ? object.props.children : [object];
}

function isElementOfType(object, type) {
    return isElement(object) && object.type === type;
}
