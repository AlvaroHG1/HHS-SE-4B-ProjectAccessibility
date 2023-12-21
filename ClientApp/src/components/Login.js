import {Component} from "react";
import './Login.css';
export class Login extends Component {

    
    render() {
        return (
            <div className="login-container">
                <div>
                <h1>Login</h1>
                <h2>Gebruikersnaam</h2>
                <input type="text" placeholder="Gebruikersnaam"></input>
                <h2>Wachtwoord</h2>
                <input type="password" placeholder="Wachtwoord"></input>
                <h2>Wachtwoord vergeten?</h2>
                </div>
                <div className="login-buttons">
                <button>Inloggen</button>
                <button>Aanmelden</button>
                </div>
                    
            </div>
        );
    }
    
}