import React, { Component } from "react";
import './RegisterBedrijf.css';
import { Navigate } from "react-router-dom";
export class RegisterBedrijf extends Component {
    constructor(props) {
        super(props);
        this.state = {
            bedrijfsnaam: "",
            locatie: "",
            email: "",
            wachtwoord: "",
            bevestigWachtwoord: "", 
            redirectToBedrijvenPortal: false
        };
    }

    handleInputChange = (event) => {
        const { name, value } = event.target;
        this.setState({
            [name]: value
        });
    }

    handleSubmit = (event) => {
        event.preventDefault();

        const { bedrijfsnaam, locatie, email, wachtwoord, bevestigWachtwoord } = this.state;

        // Check of wachtwoord en bevestiging overeenkomen
        if (wachtwoord !== bevestigWachtwoord) {
            alert("Wachtwoord en bevestig wachtwoord komen niet overeen.");
            return;
        }

        // Verstuur de gegevens naar de backend
        fetch('https://localhost:7216/api/Bedrijf', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                Naam: bedrijfsnaam,
                Link: 'bedrijflink',
                Bedrijfsinformatie: 'BedrijfsInformatie',
                Locatie: locatie,
                Email: email,
                Wachtwoord: wachtwoord
            })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Er ging iets mis bij het registreren van het bedrijf.');
                }
                return response.json();
            })
            .then(data => {
                // Als het goed gaad:
                console.log('Bedrijf succesvol geregistreerd:', data);
                this.setState({ redirectToBedrijvenPortal: true });
            })
            .catch(error => {
                // als er een error optreed:
                console.error('Er ging iets mis:', error);
                alert('Er ging iets mis bij het registreren van het bedrijf. Probeer het later opnieuw.');
            });
    }

    render() {
        const { bedrijfsnaam, locatie, email, wachtwoord, bevestigWachtwoord, redirectToBedrijvenPortal } = this.state;
        
        if (redirectToBedrijvenPortal) {
            return <Navigate to="/bedrijvenPortal" />;
        }
        return (
            <div>
                <div className="register-bedrijf-container">
                    <h1>Registreer Bedrijf</h1>
                    <form onSubmit={this.handleSubmit}>
                        <h2>Bedrijfsnaam</h2>
                        <input type="text" name="bedrijfsnaam" value={bedrijfsnaam} placeholder="Voer uw bedrijfsnaam in..." onChange={this.handleInputChange} required />
                        <h2>Locatie</h2>
                        <input type="text" name="locatie" value={locatie} placeholder="Voer uw bedrijfsadres in. Geen fysieke locatie? Vul 'online' in..." onChange={this.handleInputChange} required />
                        <h2>Email-adres</h2>
                        <input type="email" name="email" value={email} placeholder="Voer uw bedrijfs-email in..." onChange={this.handleInputChange} required />
                        <h2>Wachtwoord</h2>
                        <input type="password" name="wachtwoord" value={wachtwoord} placeholder="Voer uw wachtwoord in..." onChange={this.handleInputChange} required />
                        <h2>Bevestig wachtwoord</h2>
                        <input type="password" name="bevestigWachtwoord" value={bevestigWachtwoord} placeholder="Voer uw wachtwoord opnieuw in..." onChange={this.handleInputChange} required />
                        <button type="submit" className="registreer-bedrijf-button">Registreer</button>
                    </form>
                </div>
            </div>
        );
    }
}
