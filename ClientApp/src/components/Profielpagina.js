import React, { Component } from 'react';
import './Profielpagina.css';
export class Profielpagina extends Component {

        constructor(props) {
                super(props);
                this.state = {
                        ervaringdeskundige: null,
                };
        }
        getUserId() {
            return '3';
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
                    console.log('Fetch response:', response);
                    if (!response.ok) {
                            console.error(`Error: ${response.status} - ${response.statusText}`);
                            return;
                    }
                    
                    const ervaringdeskundigeData = await response.json();
                    
                    this.setState({ ervaringdeskundige: ervaringdeskundigeData });
                    console.log('ervaringdeskundige:', ervaringdeskundigeData);
                        
                } catch (error) {
                        console.error('Exception during fetch:', error); 
                }
        }
                
    render() {
        const {ervaringdeskundige} = this.state;
        console.log('ervaringdeskundige:', ervaringdeskundige);
        if (!ervaringdeskundige) {
                return <div>Loading...</div>; 
        }
        return (
            <div className="Profielpagina-container">
                <h1>{`Profielpagina: ${ervaringdeskundige.ervaringdeskundige.voornaam} ${ervaringdeskundige.ervaringdeskundige.achternaam}`}</h1>
                <h2>Contact</h2>
                    <div className="Info-container">
                            <div>
                                    <h3>Telefoonnummer:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.ervaringdeskundige.telefoonnummer}`}</h4>
                            </div>
                    </div>
                    <div className="Info-container">
                            <div>
                                    <h3>Email:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.ervaringdeskundige.email}`}</h4>
                            </div>
                    </div>
                <h2>Adres</h2>
                    <div className="Info-container">
                            <div>
                                    <h3>Straatnaam:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.ervaringdeskundige.straatnaam}`}</h4>
                            </div>
                    </div>
                    <div className="Info-container">
                            <div>
                                    <h3>Huisnummer:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.ervaringdeskundige.huisnummer}`}</h4>
                            </div>
                    </div>
                    <div className="Info-container">
                            <div>
                                    <h3>Plaatsnaam:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.ervaringdeskundige.plaats}`}</h4>
                            </div>
                    </div>
                    <div className="Info-container">
                            <div>
                                    <h3>Postcode:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.ervaringdeskundige.postcode}`}</h4>
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
                
            <div className="Info-container2">
                    <div>
                            <h2>ContactVoorkeur: </h2>
                    </div>
                    <div>
                            <h4 className = 'specialh4'>{ervaringdeskundige.ervaringdeskundige.contactvoorkeur || 'geen'}</h4>
                    </div>
            </div>
            <div className="Info-container2">
                    <div>
                            <h2>Commercieel geïnteresseerd? </h2>
                    </div>
                    <div>
                            <h4 className = 'specialh4'>{ervaringdeskundige.ervaringdeskundige.commercieel ? 'JA' : 'NEE'}</h4>
                    </div>
            </div>
                <h2>Beschikbaarheid</h2>
            </div>
        );
    }
}