import React, { Component } from "react";
import { Navigate } from "react-router-dom";
import './Login.css';
import { Link } from "react-router-dom";
import { NavLink } from "reactstrap";

export class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: '',
            password: '',
            loginError: false,
            redirectToResearch: false,
        };
    }

    handleEmailChange = (event) => {
        this.setState({ email: event.target.value, loginError: false });
    }

    handlePasswordChange = (event) => {
        this.setState({ password: event.target.value, loginError: false });
    }

    handleLoginClick = async () => {
        try {
            const response = await fetch(`https://localhost:7216/api/Login/${encodeURIComponent(this.state.email)}, ${encodeURIComponent(this.state.password)}`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
            });

            if (response.status === 200) {
                console.log('Login successful!');
                this.setState({ redirectToResearch: true });
            } else {
                console.log('Login failed!');
                this.setState({ loginError: true });
            }
        } catch (error) {
            console.error('Error during login:', error);
        }
    }

    render() {
        if (this.state.redirectToResearch) {
            return <Navigate to="/onderzoeken" />;
        }
        return (
            <div>
                <div className="login-container">
                    <h1>Login</h1>
                    <h2>Email:</h2>
                    <input type="text" placeholder="Email" onChange={this.handleEmailChange}></input>
                    <h2>Wachtwoord:</h2>
                    <input type="password" placeholder="Wachtwoord" onChange={this.handlePasswordChange}></input>

                    {this.state.loginError && <p style={{ color: 'red' }}>Onjuiste email of wachtwoord. Probeer opnieuw.</p>}

                    <div className="login-buttons">
                        <button className="login-button" onClick={this.handleLoginClick}>inloggen</button>
                        <button className="signup-button"><NavLink tag={Link} className="signup-link" to="/registreer">aanmelden</NavLink></button>
                    </div>
                    <div>
                        <a href='https://localhost:44466/registreer'>
                            <h3>Wachtwoord vergeten?</h3>
                        </a>
                    </div>
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