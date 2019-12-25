import React, { useState } from 'react';
import { createGlobalStyle } from 'styled-components';
import { Link } from 'react-router-dom';
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

export default function NavMenu() {
    const [collapsed, setCollapsed] = useState(true);

    return (
        <>
            <NavMenuStyling />
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">Randomizer</NavbarBrand>
                        <NavbarToggler className="mr-2" onClick={() => setCollapsed(!collapsed)} />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" navbar isOpen={!collapsed}>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/instructions">Instructions</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/configure/smz3">Create randomized game</NavLink>
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        </>
    );
}
