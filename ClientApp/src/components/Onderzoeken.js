import React, {Component} from 'react';
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
            const responseOnderzoeken = await fetch('https://localhost:7216/api/GetOnderzoeken/4', {
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
            const initialSelectedOnderzoek = openOnderzoeken[0];

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
        this.setState({isLoading: true});

        try {
            const response = await fetch(`https://localhost:7216/api/Onderzoek/${onderzoek.ocode}`);

            if (!response.ok) {
                console.error(`Failed to fetch data: ${response.statusText}`);
            }

            const data = await response.json();
            this.setState({
                selectedOnderzoek: onderzoek,
                onderzoekData: data,
                isLoading: false,
                error: null,
            });
        } catch (error) {
            console.error('Error fetching data:', error);
            this.setState({
                isLoading: false,
                error: 'Failed to fetch data. Please try again.',
            });
        }
    };

    render() {
        const {openOnderzoeken, selectedOnderzoek, onderzoekData, loading} = this.state;

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
                                <div className="onderzoek-titel">
                                    <h1>{onderzoekData.titel}</h1>
                                    <br/>
                                    <br/>
                                </div>
                                <div className="beschrijving-container">
                                    <h2>Beschrijving: </h2>
                                    <h3>{onderzoekData.beschrijving}</h3>
                                </div>
                                <div className="beperking-container">
                                    <h2>Gezochte beperking:</h2>
                                    <h3>{onderzoekData.gezochteBeperking}</h3>
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