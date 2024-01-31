import React, { Component } from "react";
import './RegisterErvaringdeskundige.css';
import { Navigate } from "react-router-dom";

export class RegisterErvaringdeskundige extends Component {
    constructor(props) {
        super(props);
        this.state = {
            voornaam: "",
            achternaam: "",
            geboortedatum: "",
            straatnaam: "",
            huisnummer: "",
            plaats: "",
            postcode: "",
            email: "",
            wachtwoord: "",
            redirectToOnderzoeken: false
        };
    }

    handleInputChange = (event) => {
        const { name, value } = event.target;
        // dit is nodig voor geboortedatum invoer veld.
        if (name === "geboortedatum") {
            const date = new Date(value);
            const isoDateString = date.toISOString();
            this.setState({
                [name]: isoDateString
            });
        }
        else {
            this.setState({
                [name]: value
            });
        }
    }

    handleSubmit = (event) => {
        event.preventDefault();

        const { voornaam, achternaam, geboortedatum, straatnaam, huisnummer, plaats, postcode, email, wachtwoord } = this.state;

        // Verstuur de gegevens naar de backend
        fetch('https://localhost:7216/api/Ervaringdeskundige', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                Voornaam: voornaam,
                Achternaam: achternaam,
                Geboortedatum: geboortedatum,
                Straatnaam: straatnaam,
                Huisnummer: huisnummer,
                Plaats: plaats,
                Postcode: postcode,
                Telefoonnummer: '', 
                Commercieel: true, 
                Contactvoorkeur: '', 
                Email: email,
                Wachtwoord: wachtwoord
            })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Er ging iets mis bij het registreren van de ervaringsdeskundige.');
                }
                return response.json();
            })
            .then(data => {
                // Als het goed gaat:
                console.log('Ervaringsdeskundige succesvol geregistreerd:', data);
                this.setState({ redirectToOnderzoeken: true });
            })
            .catch(error => {
                // Als er een error optreedt:
                console.error('Er ging iets mis:', error);
                alert('Er ging iets mis bij het registreren van de ervaringsdeskundige. Probeer het later opnieuw.');
            });
    }

    render() {
        const { voornaam, achternaam, geboortedatum, straatnaam, huisnummer, plaats, postcode, email, wachtwoord, redirectToOnderzoeken } = this.state;

        if (redirectToOnderzoeken) {
            return <Navigate to="/onderzoeken" />;
        }
        return (
            <div>
                <div className="register-ervaringdeskundige-container">
                    <h1>Registreer Ervaringsdeskundige</h1>
                    <form onSubmit={this.handleSubmit}>
                        <h2>Voornaam</h2>
                        <input type="text" name="voornaam" value={voornaam} placeholder="Voer uw voornaam in..." onChange={this.handleInputChange} />
                        <h2>Achternaam</h2>
                        <input type="text" name="achternaam" value={achternaam} placeholder="Voer uw achternaam in..." onChange={this.handleInputChange} />
                        <h2>Geboortedatum</h2>
                        <input type="date" name="geboortedatum" value={geboortedatum} onChange={this.handleInputChange} />
                        <h2>Straatnaam</h2>
                        <input type="text" name="straatnaam" value={straatnaam} placeholder="Voer uw straatnaam in..." onChange={this.handleInputChange} />
                        <h2>Huisnummer</h2>
                        <input type="text" name="huisnummer" value={huisnummer} placeholder="Voer uw huisnummer in..." onChange={this.handleInputChange} />
                        <h2>Plaats</h2>
                        <input type="text" name="plaats" value={plaats} placeholder="Voer uw plaatsnaam in..." onChange={this.handleInputChange} />
                        <h2>Postcode</h2>
                        <input type="text" name="postcode" value={postcode} placeholder="Voer uw postcode in..." onChange={this.handleInputChange} />
                        <h2>Email-adres</h2>
                        <input type="email" name="email" value={email} placeholder="Voer uw email-adres in..." onChange={this.handleInputChange} />
                        <h2>Wachtwoord</h2>
                        <input type="password" name="wachtwoord" value={wachtwoord} placeholder="Voer uw wachtwoord in..." onChange={this.handleInputChange} />
                        <button type="submit" className="registreer-ervaringdeskundige-button">Registreer</button>
                    </form>
                </div>
            </div>
        );
    }
}
