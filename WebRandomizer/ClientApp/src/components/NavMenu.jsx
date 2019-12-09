import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import './NavMenu.css';

export default function NavMenu() {
    const [collapsed, setCollapsed] = useState(true);

    return (
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
                                <NavLink tag={Link} className="text-dark" to="/randomizer">Create randomized game</NavLink>
                            </NavItem>
                        </ul>
                    </Collapse>
                </Container>
            </Navbar>
        </header>
    );
}
