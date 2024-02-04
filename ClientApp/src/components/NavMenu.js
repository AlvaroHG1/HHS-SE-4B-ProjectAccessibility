import React, { Component } from 'react';
import {Collapse, List, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink} from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';


export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
      isMenuOpen: false,
      expanded: false,
    };
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed,
      isMenuOpen: !this.state.isMenuOpen,
      expanded: !this.state.expanded
    });
  }

  closeNavbar() {
    this.setState({
      collapsed: true,
      isMenuOpen: false,
      expanded: false,
    });
  }


  render() {
    return (
        <header>
          <Navbar
              className="navbar-expand-sm navbar-toggleable-sm bg-blackborder-bottom box-shadow mb-3"
              container
              light
          >
            <img src="/image/Logoacces.jpg" alt="Logo" className="logo-image" />
            <a  href="https://localhost:44466/"> 
              <NavbarBrand to="/">ProjectAccessibility</NavbarBrand></a>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <div className="container nav-container">
              <div className="hamburger-lines" onClick={this.toggleNavbar}>
                <span className="line line1"></span>
                <span className="line line2"></span>
                <span className="line line3"></span>
              </div>
              <Collapse
                  className={`d-sm-inline-flex flex-sm-row-reverse  menu-items ${
                      this.state.expanded ? 'expanded' : 'collapsed'
                  }`}
                  isOpen={!this.state.collapsed}
                  navbar
              >
                <ul className="navbar-nav flex-grow">
                  <li>
                  <NavItem>
                    <NavLink tag={Link} className="text-white" to="/" onClick={() => this.closeNavbar()}>Home
                    </NavLink>
                  </NavItem>
                  <NavItem>
                    <NavLink tag={Link} className="text-white" to="/login" onClick={() => this.closeNavbar()}>Login
                    </NavLink>
                  </NavItem>
                  <NavItem>
                    <NavLink tag={Link} className="text-white" to="/profielpagina" onClick={() => this.closeNavbar()}>Profielpagina
                    </NavLink>
                  </NavItem>
                  <NavItem>
                    <NavLink tag={Link} className="text-white" to="/chats" onClick={() => this.closeNavbar()}>Chats
                    </NavLink>
                  </NavItem>
                    <NavItem>
                      <NavLink tag={Link} className="text-white" to="/onderzoeken" onClick={() => this.closeNavbar()}>Onderzoeken
                      </NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={Link} className="text-white" to="/bedrijvenPortal" onClick={() => this.closeNavbar()}>Bedrijven Portal
                      </NavLink>
                    </NavItem>
                    <NavItem>
                      <NavLink tag={Link} className="text-white" to="/beheerdersportal" onClick={() => this.closeNavbar()}>Beheerders Portal
                      </NavLink>
                    </NavItem>
                    </li>
                </ul>
              </Collapse>
            </div>
            <a href="https://localhost:44466/Login">
              <img src="/image/man%20wit.png" alt="Login" className="login-image"/>
            </a>
          </Navbar>
        </header>
    );
  }
}