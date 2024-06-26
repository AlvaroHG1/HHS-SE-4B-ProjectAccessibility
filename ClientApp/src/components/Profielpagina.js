import React, { Component } from 'react';
import './Profielpagina.css';

export class Profielpagina extends Component {
    constructor(props) {
        super(props);
        this.state = {
            ervaringdeskundige: null,
            bewerkingMode: false,
            bewerkteGegevens: {
                voornaam: '',
                achternaam: '',
                email: '',
                telefoonnummer: '',
                straatnaam: '',
                huisnummer: '',
                plaats: '',
                postcode: '',
                commercieel: false,
                contactvoorkeur: '',
                geboortedatum: '',
            },
            password: '',
            verwijderButtonClicked: false,
        };
    }

    getUserId() {
        return '4';
    }

    toggleBewerkingsmodus = () => {
        this.setState(prevState => ({
            bewerkingMode: !prevState.bewerkingMode,
            bewerkteGegevens: {
                ...this.state.ervaringdeskundige.ervaringdeskundige 
            }
        }));
    }

    handlePasswordChange = (event) => {
        this.setState({ password: event.target.value });
    }

    handleVerwijderAccountClick = () => {
        this.setState({ verwijderButtonClicked: true });
    }
    handleAnnulerenClick = () => {
        this.setState({ verwijderButtonClicked: false });
    }
    handleVerwijderAccount = async () => {
        if (!this.state.password) {
            alert('Vul alstublieft uw wachtwoord in om jouw ervaringsdeskundige account te verwijderen.');
            return;
        }

        try {
            const email = this.state.ervaringdeskundige.ervaringdeskundige.email;
            const response = await fetch(`https://localhost:7216/api/CheckWachtwoord/${encodeURIComponent(email)},${encodeURIComponent(this.state.password)}`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
            });
            if (!response.ok) {
                console.error(`Error: ${response.status} - ${response.statusText}`);
                return;
            }

            const isPasswordCorrect = await response.json();

            if (isPasswordCorrect) {
                const userId = this.getUserId();
                const deleteResponse = await fetch(`https://localhost:7216/api/Ervaringdeskundige/${userId}`, {
                    method: 'DELETE',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    }
                });

                if (!deleteResponse.ok) {
                    console.error(`Error: ${deleteResponse.status} - ${deleteResponse.statusText}`);
                    return;
                }

                window.location.href = '/';
            } else {
                alert('Het ingevoerde wachtwoord is onjuist :(');
            }
        } catch (error) {
            console.error('Exception during fetch:', error);
        }
    }


    async componentDidMount() {
        try {
            const userId = this.getUserId();

            const response = await fetch(`https://localhost:7216/api/Ervaringdeskundige/${userId}`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                console.error(`Error: ${response.status} - ${response.statusText}`);
                return;
            }

            const ervaringdeskundigeData = await response.json();

            this.setState({ ervaringdeskundige: ervaringdeskundigeData });
        } catch (error) {
            console.error('Exception during fetch:', error);
        }
    }

    handleInputChange = (event) => {
        const { name, value, type, checked } = event.target;
        const newValue = type === 'checkbox' ? checked : value;
        this.setState(prevState => ({
            bewerkteGegevens: {
                ...prevState.bewerkteGegevens,
                [name]: newValue
            }
        }));
    }

    toggleCommercieel = () => {
        this.setState(prevState => ({
            bewerkteGegevens: {
                ...prevState.bewerkteGegevens,
                commercieel: !prevState.bewerkteGegevens.commercieel
            }
        }));
    }

    handleSubmit = async (event) => {
        event.preventDefault();
        const userId = this.getUserId();
        const bewerkteGegevensCopy = { ...this.state.bewerkteGegevens };
        // Converteert de geboortedatum naar het ISO-formaat
        bewerkteGegevensCopy.geboortedatum = new Date(bewerkteGegevensCopy.geboortedatum).toISOString();
        try {
            const response = await fetch(`https://localhost:7216/api/Ervaringdeskundige/${userId}`, {
                method: 'PUT',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(bewerkteGegevensCopy)
            });
            if (!response.ok) {
                console.error(`Error: ${response.status} - ${response.statusText}`);
                return;
            }
            // Update de staat met de bewerkte gegevens
            this.setState(prevState => ({
                ervaringdeskundige: {
                    ...prevState.ervaringdeskundige,
                    ervaringdeskundige: {
                        ...prevState.ervaringdeskundige.ervaringdeskundige,
                        ...prevState.bewerkteGegevens
                    }
                },
                bewerkingMode: false
            }));
        } catch (error) {
            console.error('Exception during fetch:', error);
        }
    }
                
    render() {
        const {ervaringdeskundige, bewerkingMode, bewerkteGegevens, verwijderButtonClicked} = this.state;
        console.log('ervaringdeskundige:', ervaringdeskundige);
        if (!ervaringdeskundige) {
                return <div>Loading...</div>; 
        }
        return (
            <div className="Profielpagina-container">
                <div className="button-container">
                    <button onClick={this.toggleBewerkingsmodus} className="bewerk-button">
                        {bewerkingMode ? 'Annuleren' : 'Bewerken'}
                    </button>
                    {bewerkingMode && (
                        <button onClick={this.handleSubmit} className="bewerk-button">
                            Submit
                        </button>
                    )}
                </div>
                {verwijderButtonClicked && (
                    <div className="delete-account-container1">
                        <div className="delete-account-container2">
                            <h2 style = {{marginTop: "-1rem"}}>Voer uw wachtwoord in om jouw ervaringsdeskundige account te verwijderen:</h2>
                            <input
                                type="password"
                                value={this.state.password}
                                onChange={this.handlePasswordChange}
                                className="delete-account-input"
                            />
                            <div className="delete-account-buttons">
                                <button onClick={this.handleVerwijderAccount} className="delete-account-button">Delete Account</button>
                                <button onClick={this.handleAnnulerenClick} className="delete-account-button">Annuleren</button>
                            </div>
                        </div>
                    </div>
                )}
                {!verwijderButtonClicked && (
                    <button onClick={this.handleVerwijderAccountClick} className="verwijder-button">
                        Verwijder Account
                    </button>
                )}
                <h1>
                    {bewerkingMode ? (
                        <div>
                            <h1>Profielpagina</h1>
                            <div style={{ display: "flow"}}>
                                <div style={{ display: "flex"}}>
                                    <h3 style={{marginRight: "1.5em"}}>Voornaam: </h3>
                                    <input
                                        type="text"
                                        name="voornaam"
                                        value={bewerkteGegevens.voornaam}
                                        onChange={this.handleInputChange}
                                        className={"input-field"}
                                    />
                                </div>
                                <div style={{ display: "flex"}}>
                                    <h3 style={{marginRight: "1.5em"}}>Achternaam: </h3>
                                    <input
                                        type="text"
                                        name="achternaam"
                                        value={bewerkteGegevens.achternaam}
                                        onChange={this.handleInputChange}
                                        className={"input-field"}
                                    />
                                </div>
                            </div>
                        </div>
                    ) : (
                        `Profielpagina ${ervaringdeskundige.ervaringdeskundige.voornaam} ${ervaringdeskundige.ervaringdeskundige.achternaam}`
                    )}
                </h1>
                <h2>Contact</h2>
                <div className="Info-container">
                    <div>
                        <h3>Telefoonnummer:</h3>
                    </div>
                    <div>
                        {bewerkingMode ? (
                            <input
                                type="text"
                                name="telefoonnummer"
                                value={bewerkteGegevens.telefoonnummer}
                                onChange={this.handleInputChange}
                                style={{ width: "15em"}}
                            />
                        ) : (
                            <h4>{`${ervaringdeskundige.ervaringdeskundige.telefoonnummer}`}</h4>
                        )}
                    </div>
                </div>
                <div className="Info-container">
                    <div>
                        <h3>Email:</h3>
                    </div>
                    <div>
                        {bewerkingMode ? (
                            <input
                                type="text"
                                name="email"
                                value={bewerkteGegevens.email}
                                onChange={this.handleInputChange}
                                style={{ width: "15em"}}
                            />
                        ) : (
                            <h4>{`${ervaringdeskundige.ervaringdeskundige.email}`}</h4>
                        )}
                    </div>
                </div>
                <h2>Adres</h2>
                <div className="Info-container">
                    <div>
                        <h3>Straatnaam:</h3>
                    </div>
                    <div>
                        {bewerkingMode ? (
                            <input
                                type="text"
                                name="straatnaam"
                                value={bewerkteGegevens.straatnaam}
                                onChange={this.handleInputChange}
                                style={{ width: "15em"}}
                            />
                        ) : (
                            <h4>{`${ervaringdeskundige.ervaringdeskundige.straatnaam}`}</h4>
                        )}
                    </div>
                </div>
                <div className="Info-container">
                    <div>
                        <h3>Huisnummer:</h3>
                    </div>
                    <div>
                        {bewerkingMode ? (
                            <input
                                type="text"
                                name="huisnummer"
                                value={bewerkteGegevens.huisnummer}
                                onChange={this.handleInputChange}
                                style={{ width: "15em"}}
                            />
                        ) : (
                            <h4>{`${ervaringdeskundige.ervaringdeskundige.huisnummer}`}</h4>
                        )}
                    </div>
                </div>
                <div className="Info-container">
                    <div>
                        <h3>Plaatsnaam:</h3>
                    </div>
                    <div>
                        {bewerkingMode ? (
                            <input
                                type="text"
                                name="plaats"
                                value={bewerkteGegevens.plaats}
                                onChange={this.handleInputChange}
                                style={{ width: "15em"}}
                            />
                        ) : (
                            <h4>{`${ervaringdeskundige.ervaringdeskundige.plaats}`}</h4>
                        )}
                    </div>
                </div>
                <div className="Info-container">
                    <div>
                        <h3>Postcode:</h3>
                    </div>
                    <div>
                        {bewerkingMode ? (
                            <input
                                type="text"
                                name="postcode"
                                value={bewerkteGegevens.postcode}
                                onChange={this.handleInputChange}
                                style={{ width: "15em"}}
                            />
                        ) : (
                            <h4>{`${ervaringdeskundige.ervaringdeskundige.postcode}`}</h4>
                        )}
                    </div>
                </div>
                <h2>Type Beperking(en)</h2>
                    <ul>
                            {ervaringdeskundige.beperkingen.map(beperking => (
                                <li key={beperking}>{`${beperking}`}</li>
                            ))}
                    </ul>
                <h2>Hulpmiddel(en)</h2>
                    <ul>
                            {ervaringdeskundige.hulpmiddellen.map(hulpmiddel => (
                                <li key={hulpmiddel}>{`${hulpmiddel}`}</li>
                            ))}
                    </ul>
                <h2>Aandoening/ziektes</h2>
                    <ul>
                            {ervaringdeskundige.aandoeningen.map(aandoening => (
                                <li key={aandoening}>{`${aandoening}`}</li>
                            ))}
                    </ul>
                <h2>Type onderzoek voorkeur:</h2>
                    <ul>
                        {ervaringdeskundige.voorkeurTypes.map(onderzoekvoorkeur => (
                            <li key={onderzoekvoorkeur}>{`${onderzoekvoorkeur}`}</li>
                        ))}
                    </ul>
            <div className="Info-container">
                <div>
                    <h2>Contact voorkeur:</h2>
                </div>
                <div>
                    {bewerkingMode ? (
                        <input
                            type="text"
                            name="contactvoorkeur"
                            value={bewerkteGegevens.contactvoorkeur}
                            onChange={this.handleInputChange}
                            style={{ width: "15em", marginTop: "3em"}}
                        />
                    ) : (
                        <h4 className = 'specialh4'>{ervaringdeskundige.ervaringdeskundige.contactvoorkeur || 'geen'}</h4>
                    )}
                </div>
            </div>
            <div className="Info-container">
                <div>
                    <h2 style = {{paddingBottom: "1em"}}>Commercieel geïntresseerd?</h2>
                </div>
                <div>
                    {bewerkingMode ? (
                        <button onClick={this.toggleCommercieel} className="Janee-button">
                            {bewerkteGegevens.commercieel ? 'Ja' : 'Nee'}
                        </button>
                    ) : (
                        <h4 className='specialh4' style = {{paddingBottom: "1em"}}>{ervaringdeskundige.ervaringdeskundige.commercieel ? 'Ja' : 'Nee'}</h4>
                    )}
                </div>
            </div>
            </div>
        );
    }
}