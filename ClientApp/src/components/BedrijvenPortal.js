import React, {Component} from 'react';
import './BedrijvenPortal.css';
import {Link} from "react-router-dom";

export class BedrijvenPortal extends Component {
    
    render() {
        return ( 
            <div className="Desktop10" style={{ width: 1920, height: 2043, position: 'relative', background: '#131B56' }}>
                <div className="BedrijvenPortal" style={{ left: 520, top: 1, position: 'absolute', textAlign: 'center', color: 'white', fontSize: 64, fontFamily: 'Roboto', fontWeight: '700', wordWrap: 'break-word' }}>Bedrijven Portal</div>
                        <button className="button onderzoekspagina-button" onClick={() => this.handleButtonClick('OnderzoeksPagina')}>
                            <div className="text onderzoekspagina-text">Onderzoeks Pagina</div>
                        </button>
                        <button className="button chats-button" onClick={() => this.handleButtonClick('Chats')}>
                            <div className="text chats-text">Chats</div>
                        </button>
                        <Link to={"/ProfielUpdaten"} className="button profiel-updaten-button" onClick={() => this.handleButtonClick('ProfielUpdaten')}>
                            <div className="text profiel-updaten-text">Profiel Updaten</div>
                        </Link>
                        <button className="button info-clickstream-button" onClick={() => this.handleButtonClick('InfoClickstream')}>
                            <div className="text info-clickstream-text">Info clickstream</div>
                        </button>
            </div>)
    }
}