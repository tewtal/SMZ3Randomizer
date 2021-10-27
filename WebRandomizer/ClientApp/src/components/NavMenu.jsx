import React, { useState, useContext } from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import classNames from 'classnames';

import { Container, Navbar, NavbarBrand, NavbarToggler, Collapse, Nav, NavItem, NavLink } from 'reactstrap';
import { UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

import { GameTraitsCtx } from '../game/traits';

const StyledNavbar = styled(Navbar)`
  box-shadow: 0 .25rem .75rem rgba(0, 0, 0, .05);
`;

const StyledNavbarBrand = styled(NavbarBrand)`
  white-space: normal;
  text-align: center;
  word-break: break-all;
`;

function StyledNavLink({ className, children, ...props }) {
    return <NavLink className={classNames("text-dark", className)} {...props}>{children}</NavLink>;
}

export default function NavMenu() {
    const [showMenu, setShowMenu] = useState(false);
    const game = useContext(GameTraitsCtx);

    return (
        <header>
            <StyledNavbar className="border-bottom mb-3" expand="sm" color="white" light>
                <Container>
                    <StyledNavbarBrand tag={Link} to="/">Home</StyledNavbarBrand>
                    <NavbarToggler className="mr-2" onClick={() => setShowMenu(!showMenu)} />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" navbar isOpen={showMenu}>
                        <Nav className="flex-grow" navbar>
                            <NavItem><StyledNavLink tag={Link} to="/configure">Generate randomized game</StyledNavLink></NavItem>
                            <UncontrolledDropdown nav inNavbar>
                                <DropdownToggle className="text-dark" nav caret>Help</DropdownToggle>
                                <DropdownMenu>
                                    <DropdownItem><StyledNavLink tag={Link} to="/information">Information</StyledNavLink></DropdownItem>
                                    <DropdownItem><StyledNavLink tag={Link} to="/mwinstructions">Multiworld instructions</StyledNavLink></DropdownItem>
                                    {game.id === 'smz3' && (
                                        <DropdownItem><StyledNavLink tag={Link} to="/logic">Logic Log</StyledNavLink></DropdownItem>
                                    )}
                                    <DropdownItem><StyledNavLink tag={Link} to="/resources">Resources</StyledNavLink></DropdownItem>
                                    <DropdownItem><StyledNavLink tag={Link} to="/changelog">Changes</StyledNavLink></DropdownItem>
                                </DropdownMenu>
                            </UncontrolledDropdown>
                        </Nav>
                    </Collapse>
                </Container>
            </StyledNavbar>
        </header>
    );
}
