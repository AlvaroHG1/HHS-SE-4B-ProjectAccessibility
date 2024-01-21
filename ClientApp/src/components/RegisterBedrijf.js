import {Component} from "react";
import './RegisterBedrijf.css';
export class RegisterBedrijf extends Component {

    render() {
        return (
            <div>
                <div className="register-bedrijf-container">
                    <h1>Registreer Bedrijf</h1>
                    <h2>Bedrijfsnaam</h2>
                    <input type="text" placeholder="Voer uw bedrijfsnaam in..." onChange={null}></input>
                    <h2>Locatie</h2>
                    <input type="text" placeholder="voer uw bedrijfs-adress in. geen fysieke locatie? vul 'online' in..." onChange={null}></input>
                    <h2>Email-adres</h2>
                    <input type="text" placeholder="voer uw bedrijfs-email in..." onChange={null}></input>
                    <h2>Wachtwoord</h2>
                    <input type="password" placeholder="Voer uw wachtwoord in..." onChange={null}></input>
                    <h2>Bevestig wachtwoord</h2>
                    <input type="password" placeholder="Voer uw wachtwoord opnieuw in..." onChange={null}></input>
                    <button className="registreer-bedrijf-button">Registreer</button>
                </div>
            </div>
        );
    }
}
