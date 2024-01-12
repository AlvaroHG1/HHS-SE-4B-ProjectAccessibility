import React, {Component} from "react";
import './Login.css';
import {Link} from "react-router-dom";
import {NavLink} from "reactstrap";
export class Login extends Component {
    
    render() {
        return (
        <div>
            <div className="login-container">
                    <h1>Login</h1>
                    <h2>Email:</h2>
                    <input type="text" placeholder="Email" onChange={this.handleEmailChange}></input>
                    <h2>Wachtwoord:</h2>
                    <input type="password" placeholder="Wachtwoord" onChange={this.handlePasswordChange}></input>
                <div className="login-buttons">
                    <button className="login-button" onClick={this.handleLoginClick}>Inloggen</button>
                    <button className="signup-button"><NavLink tag={Link} className="signup-link" to="/registreer">aanmelden</NavLink></button>
                </div>
                <h3>Wachtwoord vergeten?</h3>
            </div>
            <div className="login-met-container">
                <h4>Of log in met:</h4>
                <div className="image-container">
                    <img src="https://www.paredro.com/wp-content/uploads/2017/08/microsoft-redesign-logo.png" alt="Image 1" />
                    <img src="https://historiek.net/wp-content/uploads-phistor1/2015/09/Het-nieuw-logo-van-Google-e1441130561430.jpg" alt="Image 2" />
                </div>
            </div>
        </div>
        );
    }
}