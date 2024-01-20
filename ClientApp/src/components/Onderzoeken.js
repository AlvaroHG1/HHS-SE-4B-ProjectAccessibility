import React, { Component } from 'react';
import './Onderzoeken.css';

export class Onderzoeken extends Component {
    constructor(props) {
        super(props);
        this.state = {
            openOnderzoeken: [],
            selectedOnderzoek: null,
            onderzoekData: null,
            loading: true
        };
    }

    async componentDidMount() {
        try {
            const responseOnderzoeken = await fetch('https://localhost:7216/api/GetOnderzoeken/3', {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });

            if (!responseOnderzoeken.ok) {
                console.error(`Error: ${responseOnderzoeken.status} - ${responseOnderzoeken.statusText}`);
                return;
            }

            const openOnderzoeken = await responseOnderzoeken.json();
            console.log(openOnderzoeken.ocode);
            const initialSelectedOnderzoek = openOnderzoeken[0];

            // Make the initial second API call
            await this.fetchOnderzoekData(initialSelectedOnderzoek.ocode);

            this.setState({
                openOnderzoeken,
                selectedOnderzoek: initialSelectedOnderzoek,
                loading: false
            });
        } catch (error) {
            console.error('Exception during fetch:', error);
        }
    }

    fetchOnderzoekData = async (ocode) => {
        try {
            const responseOnderzoekData = await fetch(`https://localhost:7216/api/onderzoek/${ocode}`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });

            if (!responseOnderzoekData.ok) {
                console.error(`Error: ${responseOnderzoekData.status} - ${responseOnderzoekData.statusText}`);
                return;
            }

            const onderzoekData = await responseOnderzoekData.json();
            console.log('onderzoekData:', onderzoekData);

            this.setState({
                onderzoekData
            });
        } catch (error) {
            console.error('Exception during fetch:', error);
        }
    };

    handleOnderzoekClick = async (onderzoek) => {
        // Set loading state while fetching data
        this.setState({ isLoading: true });

        try {
            // Replace "your_api_endpoint" with the actual endpoint of your API
            const response = await fetch(`https://localhost:7216/api/Onderzoek/${onderzoek.ocode}`);
            
            // Check if the response is successful
            if (!response.ok) {
               console.error(`Failed to fetch data: ${response.statusText}`);
            }

            const data = await response.json();

            // Update the state with the new data and reset loading state
            this.setState({
                selectedOnderzoek: onderzoek,
                onderzoekData: data,
                isLoading: false,
                error: null, // Reset error if there was a previous one
            });
        } catch (error) {
            // Handle error and set loading state to false
            console.error('Error fetching data:', error);
            this.setState({
                isLoading: false,
                error: 'Failed to fetch data. Please try again.', // Customize the error message as needed
            });
        }
    };

    render() {
        const { openOnderzoeken, selectedOnderzoek, onderzoekData, loading } = this.state;

        if (loading) {
            return <div>foutje</div>;
        }

        return (
            <div className="total-container">
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
                </div>
                <div className="onderzoek-info">
                    <div>
                        {onderzoekData ? (
                            <>
                                <div className="onderzoek-informatie">
                                    <h1>{onderzoekData.titel}</h1>
                                    <br/>
                                    <br/>
                                    <h2>Beschrijving: </h2>
                                    <h3 className="beschrijving">{onderzoekData.beschrijving}</h3>
                                    <h2 className="gezochte-beperking">Gezochte beperking:</h2>
                                    <h3 className="gezochte-beperking2">{onderzoekData.gezochteBeperking}</h3>
                                </div>
                                <div className="date-container">
                                    <h2>Periode:</h2>
                                    <h3>{`${onderzoekData.startdatum} / ${onderzoekData.einddatum}`}</h3>
                                </div>
                                <div className="location-container">
                                    <h2>Locatie:</h2>
                                    <h3 className="location">{onderzoekData.locatie}, {onderzoekData.gezochtePostcode}</h3>
                                </div>

                                <div className="age-container">
                                    <h2>Leeftijdscategorie:</h2>
                                    <h3>{`${onderzoekData.minLeeftijd}-${onderzoekData.maxLeeftijd}`}</h3>
                                </div>
                            </>
                        ) : (
                            <p>Data is loading...</p>
                        )}
                    </div>
                </div>
            </div>
        );
    }
}