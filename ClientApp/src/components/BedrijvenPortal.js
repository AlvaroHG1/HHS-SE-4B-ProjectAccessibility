import React, {Component} from 'react';
import './BedrijvenPortal.css';
import {Link} from "react-router-dom";

export class BedrijvenPortal extends Component {
    
    render() {
        return ( 
            <div className="Desktop10" style={{ width: 1920, height: 2043, position: 'relative', background: '#131B56' }}>
                        <Link className="button onderzoekspagina-button" to={"/BedrijvenOnderzoek"}>
                            <div className="text onderzoekspagina-text">Onderzoeks Pagina</div>
                        </Link>
                        <Link className="button chats-button" to={"/Chats"} >
                            <div className="text chats-text">Chats</div>
                        </Link>
                        <Link to={"/ProfielUpdaten"} className="button profiel-updaten-button" >
                            <div className="text profiel-updaten-text">Profiel Updaten</div>
                        </Link>
                        <button className="button info-clickstream-button" onClick={() => this.handleButtonClick('InfoClickstream')}>
                            <div className="text info-clickstream-text">Info clickstream</div>
                        </button>
            </div>)
    }
}