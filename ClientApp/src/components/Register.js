import React, {Component} from "react";
import './Register.css';
import {Link} from "react-router-dom";
import {NavLink} from "reactstrap";
export class Register extends Component {

    render() {
        return (
            <div>
                <div className="register-container">
                    <h1>Registreren</h1>
                    <div className="registreer-buttons">
                        <Link className="registreer-choice-button1" to="/registreer-ervaringdeskundige">
                            Klik hier om zich te registreren als ervaringdeskundige
                        </Link>
                        <Link className="registreer-choice-button2" to="/registreer-bedrijf">
                            Klik hier om uw bedrijf te registreren op stichting accessibility
                        </Link>
                    </div>
                </div>
            </div>
        );
    }
}