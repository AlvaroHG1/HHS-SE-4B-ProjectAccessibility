import React, {Component} from 'react';
import './ProfielUpdaten.css';
import {Link} from "react-router-dom";

export class ProfielUpdaten extends Component {
    constructor(props) {
        super(props);
        this.state = {
            bedrijf: null,
        };
    }

    async componentDidMount() {
        try {
            const response = await fetch(`https://localhost:7216/api/GetBedrijf/14`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error(`Error: ${response.status} - ${response.statusText}`);
            }

            const bedrijvenData = await response.json();
            console.log('bedrijf:', bedrijvenData);

            this.setState({ bedrijf: bedrijvenData });

        } catch (error) {
            console.error('Exception during fetch:', error.message);
        }
    }


    render() {
        const {bedrijf} = this.state;
        console.log('bedrijf:', bedrijf);
        const bedrijfNaam = bedrijf?.bedrijf?.naam || 'N/A';
        const bedrijfLocatie = bedrijf?.bedrijf?.locatie || 'N/A';
        const bedrijfsinformatie = bedrijf?.bedrijf?.bedrijfsinformatie || 'N/A';
        return (
            <div className="Desktop11">
                <div className="ProfielUpdaten">Profiel Updaten</div>
                <div className="Titel Bedrijf">Bedrijf</div>
                <div className="Bedrijf">{bedrijfNaam}</div>
                <div className="Titel Informatie">Informatie</div>
                <div className="Informatie2">{bedrijfsinformatie}</div>
                <div className="Informatie3">Locatie</div>
                <div className="Informatie1">{bedrijfLocatie}</div>
                <div className="Titel Link">Link</div>
                <div className="Informatie4">www.jumbo.nl</div>
                <Link to="/BedrijvenPortal" className="OpslaanLink">
                    <div className="Opslaan">Opslaan</div>
                </Link>
            </div>
        );
    }
}