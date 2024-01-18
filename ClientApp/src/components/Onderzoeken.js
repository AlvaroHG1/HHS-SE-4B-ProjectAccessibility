import React, { Component } from 'react';
import './Onderzoeken.css';

export class Onderzoeken extends Component {
    constructor(props) {
        super(props);
        this.state = { openOnderzoeken: [], selectedOnderzoek: null, loading: true };
    }

    async componentDidMount() {
        try {
            const response = await fetch('https://localhost:7216/api/GetOnderzoeken/3', {
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

            const openOnderzoeken = await response.json();
            const initialSelectedOnderzoek = openOnderzoeken[0]; // Set the first element as initially selected
            this.setState({ openOnderzoeken, selectedOnderzoek: initialSelectedOnderzoek, loading: false });
        } catch (error) {
            console.error('Exception during fetch:', error);
        }
    }

    handleOnderzoekClick = (onderzoek) => {
        this.setState({ selectedOnderzoek: onderzoek });
    };

    render() {
        const { openOnderzoeken, selectedOnderzoek, loading } = this.state;

        if (loading) {
            return <div>foutje</div>;
        }

        return (
            <div className="onderzoeken-container">
                <div className="onderzoek-container">
                    <div className="onderzoek-list">
                        {openOnderzoeken.map((onderzoek) => (
                            <button
                                key={onderzoek.ocode}
                                className={`onderzoek-item ${selectedOnderzoek === onderzoek ? 'selected' : ''}`}
                                onClick={() => this.handleOnderzoekClick(onderzoek)}
                            >
                                <h5>{`${onderzoek.titel}`}</h5>
                                <p>{`Datum: ${onderzoek.startdatum}`}</p>
                            </button>
                        ))}
                    </div>
                </div>
                <div className="second-div">
                    <div>
                        
                    </div>
                </div>
            </div>
        );
    }
}