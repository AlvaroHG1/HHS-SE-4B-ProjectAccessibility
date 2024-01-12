import {Component} from "react";
import './RegisterErvaringdeskundige.css';
export class RegisterErvaringdeskundige extends Component {

    render() {
        return (
            <div>
                <div className="register-ervaringdeskundige-container">
                    <h1>Registreer ervaringdeskundige</h1>
                    <h2>Voornaam</h2>
                    <input type="text" placeholder="geboortedatum" onChange={null}></input>
                    <h2>Achternaam</h2>
                    <input type="text" placeholder="geboortedatum" onChange={null}></input>
                    <h2>Geboortedatum</h2>
                    <input type="text" placeholder="Voer uw geboortedatum in..." onChange={null}></input>
                    <h2>Straatnaam + huisnummer</h2>
                    <div className="street-number-container">
                        <input type="text" id="straatnaam" placeholder="Voer uw straatnaam in..." onChange={null}></input>
                        <input type="text" id="huisnummer" placeholder="Voer uw huisnummer in..." onChange={null}></input>
                    </div>
                    <h2>plaats</h2>
                    <input type="text" placeholder="Voer uw plaatsnaam in..." onChange={null}></input>
                    <h2>postcode</h2>
                    <input type="text" placeholder="Voer uw postcode in..." onChange={null}></input>
                    <h2>Email-adres</h2>
                    <input type="text" placeholder="Voer een geldige email-adres in" onChange={null}></input>
                    <h2>Wachtwoord</h2>
                    <input type="password" placeholder="Voer uw wachtwoord in..." onChange={null}></input>
                    <h2>bevestig wachtwoord</h2>
                    <input type="password" placeholder="Voer uw wachtwoord opnieuw in..." onChange={null}></input>
                    <button className="registreer-ervaringdeskundige-button">Registreer</button>
                </div>
            </div>
        );
    }
}