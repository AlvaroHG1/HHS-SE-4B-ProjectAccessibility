
import React, {Component} from 'react';
import './Home.css';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return ( /*begin v/d container*/

        <div>
            <div className="box-welkomOmschrijving">

                <h1>Welkom bij Stichting Accessibility</h1>
                <h2>Onze website "AccessiHub" werkt aan het optimaliseren
                    van de toegankelijkheid voor websites om een inclusieve
                    online ervaring te bieden aan iedereen!</h2>
            </div>

            <div className="box-meldJeAan">
                <div>
                    <h1> Meld je aan!</h1>
                </div>
                <div>
                    <h2> Ontdek hoe je morgen aan de slag kunt gaan
                    als onderzoeker</h2>
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
