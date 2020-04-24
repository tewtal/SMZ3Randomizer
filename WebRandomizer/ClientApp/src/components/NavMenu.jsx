import React, { useState } from 'react';
import styled from 'styled-components';
import { Link } from 'react-router-dom';
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

export default function NavMenu(props) {
    const { gameId } = props;

    const [showMenu, setShowMenu] = useState(false);

    const linkProps = { tag: Link, className: 'text-dark' };
    return (
        <header>
            <StyledNavbar className="border-bottom mb-3" expand="sm" color="white" light>
                <Container>
                    <StyledNavbarBrand tag={Link} to="/">Home</StyledNavbarBrand>
                    <NavbarToggler className="mr-2" onClick={() => setShowMenu(!showMenu)} />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" navbar isOpen={showMenu}>
                        <Nav className="flex-grow" navbar>
                            <NavItem>
                                <NavLink {...linkProps} to={`/configure/${gameId}`}>Generate randomized game</NavLink>
                            </NavItem>
                            <UncontrolledDropdown nav inNavbar>
                                <DropdownToggle className="text-dark" nav caret>Help</DropdownToggle>
                                <DropdownMenu>
                                    <DropdownItem><NavLink {...linkProps} to="/information">Information</NavLink></DropdownItem>
                                    <DropdownItem><NavLink {...linkProps} to="/mwinstructions">Multiworld instructions</NavLink></DropdownItem>
                                    {gameId === "smz3" && (
                                        <DropdownItem><NavLink {...linkProps} to="/logic">Logic Log</NavLink></DropdownItem>
                                    )}
                                    <DropdownItem><NavLink {...linkProps} to="/resources">Resources</NavLink></DropdownItem>
                                    <DropdownItem><NavLink {...linkProps} to="/changelog">Changes</NavLink></DropdownItem>
                                </DropdownMenu>
                            </UncontrolledDropdown>
                        </Nav>
                    </Collapse>
                </Container>
            </StyledNavbar>
        </header>
    );
}
