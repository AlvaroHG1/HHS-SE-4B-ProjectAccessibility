import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';


export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed,
      isMenuOpen: !this.state.isMenuOpen,
      expanded: !this.state.expanded
    });
  }

  


  render(){
    return (
        <header>
          <Navbar className="navbar-expand-sm navbar-toggleable-sm bg-blackborder-bottom box-shadow mb-3" container
                  light>
            <img src="/image/Logoacces.jpg" alt="Logo" className="logo-image"/>
            <NavbarBrand to="/">ProjectAccessibility</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2"/>
            <div className="container nav-container">
              <input className="checkbox" type="checkbox" id="menuToggle" checked={!this.state.collapsed}
                     onChange={this.toggleNavbar}/>
              <div className="hamburger-lines" onClick={this.toggleNavbar}>
                <span className="line line1"></span>
                <span className="line line2"></span>
                <span className="line line3"></span>
              </div>
              <Collapse
                  className={`d-sm-inline-flex flex-sm-row-reverse  menu-items ${this.state.expanded ? 'expanded' : 'collapsed'}`}
                  isOpen={!this.state.collapsed}
                  navbar
              >
                <ul className="navbar-nav flex-grow">
                  <NavItem>
                   <NavLink tag={Link} className="text-white" to="/">Home</NavLink>
                  </NavItem>
                  <NavItem>
                    <NavLink tag={Link} className="text-white" to="/Login">Login</NavLink>
                  </NavItem>
                    <NavItem>
                        <NavLink tag={Link} className="text-white" to="/Profielpagina">Profielpagina</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} className="text-white" to="/Chats">Chats</NavLink>
                    </NavItem>
                </ul>
              </Collapse>
            </div>
            <img src="/image/man%20wit.png" alt="Login" className="login-image"/>
          </Navbar>
        </header>
    );
  }
}