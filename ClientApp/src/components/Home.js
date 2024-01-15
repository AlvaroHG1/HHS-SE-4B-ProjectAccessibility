
import React, {Component} from 'react';
import './Home.css';
import {Link} from "react-router-dom";

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return ( /*begin v/d container*/

            <div>
                <div className="box-welkomOmschrijving">

                    <h1>Welkom bij Stichting Accessibility</h1>
                    <h2>Onze website "ProjectAccessibility" werkt aan het optimaliseren
                        van de toegankelijkheid voor websites om een inclusieve
                        online ervaring te bieden aan iedereen!</h2>
                </div>

                <div className="box-meldJeAan">
                    <Link className="MeldJeAanButton" to="/registreer-ervaringdeskundige">
                        Meld je aan
                    </Link>
                    <div>
                        <h2> Ontdek hoe je morgen aan de slag kunt gaan
                            als onderzoeker</h2>
                    </div>
                </div>

                <div className="box-startJeOnderzoek">
                    <div>
                        <h1> Ontdek hoe je morgen aan de slag kunt  gaan als bedrijf</h1>
                    </div>

                    <div>{/* HIER AANPASSEN OM TE REDIRECTEN NAAR registreer-bedrijf"*/}
                        <h2>Start je onderzoek!</h2>
                    </div>
                </div>

                <div className="box-welkomOmschrijving">

                    <h3>Samen met onze partners en klanten werken we
                        aan een inclusieve samenleving die toegankelijk is voor iedereen!</h3>
                </div>

            </div> /*einde v/d container*/
        );
    }
}
