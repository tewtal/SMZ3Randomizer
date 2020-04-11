import React, { useState } from 'react';
import { createGlobalStyle } from 'styled-components';
import { Link } from 'react-router-dom';
import { UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import { Container, Navbar, NavbarBrand, NavbarToggler, Collapse, NavItem, NavLink } from 'reactstrap';

const NavMenuStyling = createGlobalStyle`
  a.navbar-brand {
    white-space: normal;
    text-align: center;
    word-break: break-all;
  }

  .box-shadow {
    box-shadow: 0 .25rem .75rem rgba(0, 0, 0, .05);
  }
`;

export default function NavMenu(props) {
    const [collapsed, setCollapsed] = useState(true);
    const { gameId } = props;

    return (
        <>
            <NavMenuStyling />
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">Home</NavbarBrand>
                        <NavbarToggler className="mr-2" onClick={() => setCollapsed(!collapsed)} />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" navbar isOpen={!collapsed}>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to={`/configure/${gameId}`}>Generate randomized game</NavLink>
                                </NavItem>
                                <NavItem>
                                    <UncontrolledDropdown nav inNavbar>
                                        <DropdownToggle className="text-dark" nav caret>Help</DropdownToggle>
                                        <DropdownMenu>
                                            <DropdownItem><NavLink tag={Link} className="text-dark" to="/information">Information</NavLink></DropdownItem>
                                            <DropdownItem><NavLink tag={Link} className="text-dark" to="/mwinstructions">Multiworld instructions</NavLink></DropdownItem>
                                            {gameId === "smz3" && <DropdownItem><NavLink tag={Link} className="text-dark" to="/logic">Logic Log</NavLink></DropdownItem>}
                                            <DropdownItem><NavLink tag={Link} className="text-dark" to="/resources">Resources</NavLink></DropdownItem>
                                            <DropdownItem><NavLink tag={Link} className="text-dark" to="/changelog">Changes</NavLink></DropdownItem>
                                        </DropdownMenu>
                                    </UncontrolledDropdown>
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        </>
    );
}
