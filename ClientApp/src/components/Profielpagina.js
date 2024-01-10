import React, { Component } from 'react';
import './Profielpagina.css';
        export class Profielpagina extends Component {

                constructor(props) {
                        super(props);
                        this.state = {
                                ervaringdeskundige: null,
                                aandoeningen: []
                        };
                }

                async componentDidMount() {
                        try {
                                const response = await fetch('https://localhost:7216/api/Ervaringdeskundige/3', {
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
                                
                                const aandoeningResponse = await fetch(`https://localhost:7216/api/HeeftAandoening/${ervaringdeskundigeData.Ecode}`, {
                                        method: 'GET',
                                        headers: {
                                                'Accept': 'application/json',
                                                'Content-Type': 'application/json'
                                        }
                                });

                                if (!aandoeningResponse.ok) {
                                        console.error(`Error: ${aandoeningResponse.status} - ${aandoeningResponse.statusText}`);
                                        return;
                                }

                                const aandoeningenData = await aandoeningResponse.json();
                                this.setState({ aandoeningen: aandoeningenData});
                                
                        } catch (error) {
                                console.error('Exception during fetch:', error); 
                        }
                }
                
render() {
        const { ervaringdeskundige, aandoeningen } = this.state;
        
        if (!ervaringdeskundige) {
                return <div>ervaringdeskundige wilt niet komen opdagen godverdomme...</div>;
        }
        
        return (
            <div className="Profielpagina-container">
                <h1>{`Profielpagina: ${ervaringdeskundige.voornaam} ${ervaringdeskundige.achternaam}`}</h1>
                <h2>Contact</h2>
                    <div className="Info-container">
                            <div>
                                    <h3>Telefoonnummer:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.telefoonnummer}`}</h4>
                            </div>
                    </div>
                    <div className="Info-container">
                            <div>
                                    <h3>Email:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.email}`}</h4>
                            </div>
                    </div>
                <h2>Adres</h2>
                    <div className="Info-container">
                            <div>
                                    <h3>Straatnaam:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.straatnaam}`}</h4>
                            </div>
                    </div>
                    <div className="Info-container">
                            <div>
                                    <h3>Huisnummer:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.huisnummer}`}</h4>
                            </div>
                    </div>
                    <div className="Info-container">
                            <div>
                                    <h3>Plaatsnaam:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.plaats}`}</h4>
                            </div>
                    </div>
                    <div className="Info-container">
                            <div>
                                    <h3>Postcode:</h3>
                            </div>
                            <div>
                                    <h4>{`${ervaringdeskundige.postcode}`}</h4>
                            </div>
                    </div>
                <h2>Type Beperking(en)</h2>
                <h2>Hulpmiddel(en)</h2>
                <h2>Aandoening/ziektes</h2>
                <h2>Type onderzoek voorkeur</h2>
                <h2>Voorkeur benadering</h2>
                <h2>Commercieel geïnteresseerd </h2>
                <h2>Beschikbaarheid</h2>
            </div>
        );
    }
}