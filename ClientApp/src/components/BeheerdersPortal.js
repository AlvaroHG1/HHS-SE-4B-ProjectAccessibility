import React, {Component} from 'react';
import './BeheerdersPortal.css';

export class BeheerdersPortal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            activeTab: 'Bedrijf',
            bedrijven: [],
            ervaringsdeskundigen: [],
            beheerders: [],
            onderzoeken: [],
            editingRow: null,
            editedData: {
                Bedrijf: {},
                Ervaringsdeskundige: {},
                Beheerder: {},
                Onderzoek: {}
            },
        };
    }

    componentDidMount() {
        fetch('https://localhost:7216/api/Bedrijf')
            .then(response => response.json())
            .then(data => this.setState({bedrijven: data}))
            .catch(error => console.error('Error fetching bedrijven data:', error));

        fetch('https://localhost:7216/api/Ervaringdeskundige')
            .then(response => response.json())
            .then(data => this.setState({ervaringsdeskundigen: data}))
            .catch(error => console.error('Error fetching ervaringsdeskundigen data:', error));

        fetch('https://localhost:7216/api/Beheerder')
            .then(response => response.json())
            .then(data => this.setState({beheerders: data}))
            .catch(error => console.error('Error fetching beheerders data:', error))
        fetch('https://localhost:7216/api/Onderzoek')
            .then(response => response.json())
            .then(data => this.setState({onderzoeken: data}))
            .catch(error => console.error('Error fetching onderzoek data:', error))
    }

    handleTabChange = tab => {
        this.setState({activeTab: tab});
    };

    handleDelete = (code, activeTab) => {
        const deleteUrl = `https://localhost:7216/api/${activeTab}/${code}`;
        fetch(deleteUrl, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }
            })
            .catch(error => console.error('Error deleting row:', error));
        if (activeTab === "Bedrijf") {
            this.setState(prevState => ({
                bedrijven: prevState.bedrijven.filter(item => item.gcode !== code),
            }));
        }
        if (activeTab === "Beheerder") {
            this.setState(prevState => ({
                beheerders: prevState.beheerders.filter(item => item.gcode !== code),
            }));
        }
        if (activeTab === "Ervaringdeskundige") {
            this.setState(prevState => ({
                ervaringsdeskundigen: prevState.ervaringsdeskundigen.filter(item => item.gcode !== code),
            }));
        }
        if (activeTab === "Onderzoek") {
            this.setState(prevState => ({
                onderzoeken: prevState.onderzoeken.filter(item => item.ocode !== code),
            }))
        }
    };

    handleEditClick = (rowData, activeTab) => {
        if (activeTab === "Onderzoek") {
            this.setState((prevState) => ({
                editingRow: prevState.editingRow === rowData.ocode ? null : rowData.ocode,
                editedData: prevState.editingRow === rowData.ocode ? {} : {...rowData},
            }));
        } else {
            this.setState((prevState) => ({
                editingRow: prevState.editingRow === rowData.gcode ? null : rowData.gcode,
                editedData: prevState.editingRow === rowData.gcode ? {} : {...rowData},
            }));
        }
    };

    handleSaveEdit = (editedData, activeTab) => {
        let putPayload = '';
        if (activeTab === 'Bedrijf') {
            putPayload = {
                naam: editedData.naam,
                link: editedData.link,
                bedrijfsinformatie: editedData.bedrijfsinformatie,
                locatie: editedData.locatie,
                email: editedData.email
            };
        }

        if (activeTab === 'Ervaringdeskundige') {
            const date = new Date(editedData.geboortedatum);
            const isoDateString = date.toISOString();
            putPayload = {
                voornaam: editedData.voornaam,
                achternaam: editedData.achternaam,
                telefoonnummer: editedData.telefoonnummer,
                postcode: editedData.postcode,
                straatnaam: editedData.straatnaam,
                huisnummer: editedData.huisnummer,
                plaats: editedData.plaats,
                geboortedatum: isoDateString,
                email: editedData.email

            };
        }

        if (activeTab === 'Beheerder') {
            putPayload = {
                voornaam: editedData.voornaam,
                achternaam: editedData.achternaam,
                rol: editedData.rol,
                email: editedData.email
            };
        }

        if (activeTab === 'Onderzoek') {
            putPayload = {
                titel: editedData.titel,
                beschrijving: editedData.beschrijving,
                locatie: editedData.locatie,
                startdatum: editedData.startdatum,
                einddatum: editedData.einddatum,
                gezochteBeperking: editedData.gezochteBeperking,
                gezochtePostcode: editedData.gezochtePostcode,
                minLeeftijd: editedData.minLeeftijd,
                maxLeeftijd: editedData.maxLeeftijd
            };
        }
        
        
        let number;
        if (activeTab === "Onderzoek") {
            number = editedData.ocode;
        } else {
            number = editedData.gcode;
        }

        fetch(`https://localhost:7216/api/${activeTab}/${number}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(putPayload),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }
                return response.json();
            })
            .then(updatedData => {
                this.setState(prevState => ({
                    bedrijven: prevState.bedrijven.map(bedrijf => {
                        if (bedrijf.gcode === updatedData.gcode) {
                            return updatedData;
                        }
                        return bedrijf;
                    }),
                    beheerders: prevState.beheerders.map(beheerder => {
                        if (beheerder.gcode === updatedData.gcode) {
                            return updatedData;
                        }
                        return beheerder;
                    }),
                    ervaringsdeskundigen: prevState.ervaringsdeskundigen.map(ervaringsdeskundige => {
                        if (ervaringsdeskundige.gcode === updatedData.gcode) {
                            return updatedData;
                        }
                        return ervaringsdeskundige;
                    }),
                    onderzoeken: prevState.onderzoeken.map(onderzoek => {
                        if (onderzoek.ocode === updatedData.ocode) {
                            return updatedData;
                        }
                        return onderzoek;
                    }),
                    
                    editingRow: null,
                }));
            })
            .catch(error => {
                console.error('Error updating data:', error);
            });
    };

    handleInputChange = (e) => {
        const {name, value} = e.target;

        this.setState((prevState) => ({
            editedData: {
                ...prevState.editedData,
                [name]: value,
            },
        }));
    };

    render() {
        const {
            activeTab,
            bedrijven,
            ervaringsdeskundigen,
            beheerders,
            onderzoeken,
            editingRow,
            editedData
        } = this.state;

        return (
            <div className="tab-container">
                <div className="tab-menu">
                    <button className="beheerder-button" onClick={() => this.handleTabChange('Bedrijf')}>Bedrijf
                    </button>
                    <button className="beheerder-button"
                            onClick={() => this.handleTabChange('Ervaringdeskundige')}> Ervaringsdeskundige
                    </button>
                    <button className="beheerder-button" onClick={() => this.handleTabChange('Beheerder')}>Beheerder
                    </button>
                    <button className="beheerder-button" onClick={() => this.handleTabChange('Onderzoek')}>Onderzoeken
                    </button>
                </div>

                <div className="user-info-container">
                    {activeTab === 'Bedrijf' && (
                        <div>
                            <h1>Bedrijven</h1>
                            <table>
                                <thead>
                                <tr className="customtablerow">
                                    <th>Gcode</th>
                                    <th>Naam</th>
                                    <th>Bedrijfsinformatie</th>
                                    <th>Link</th>
                                    <th>Locatie</th>
                                    <th>Email</th>
                                    <th></th>
                                </tr>
                                </thead>
                                <tbody>
                                {bedrijven.map((bedrijf) => (
                                    <tr className="customtablerow" key={bedrijf.gcode}>
                                        <td>{bedrijf.gcode}</td>
                                        <td>
                                            {editingRow === bedrijf.gcode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="naam"
                                                       value={editedData.naam || bedrijf.naam}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                bedrijf.naam
                                            )}
                                        </td>
                                        <td className="maxcharacters">
                                            {editingRow === bedrijf.gcode ? (
                                                <input className="wide-input-textfield"
                                                       type="text"
                                                       name="bedrijfsinformatie"
                                                       value={editedData.bedrijfsinformatie || bedrijf.bedrijfsinformatie}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                bedrijf.bedrijfsinformatie
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === bedrijf.gcode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="link"
                                                       value={editedData.link || bedrijf.link}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                bedrijf.link
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === bedrijf.gcode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="locatie"
                                                       value={editedData.locatie || bedrijf.locatie}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                bedrijf.locatie
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === bedrijf.gcode ? (
                                                <input className="wide-input-textfield"
                                                       type="text"
                                                       name="email"
                                                       value={editedData.email || bedrijf.email}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                bedrijf.email
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === bedrijf.gcode ? (
                                                <>
                                                    <button className="uis--check"
                                                            onClick={() => this.handleSaveEdit(editedData, activeTab)}></button>
                                                </>
                                            ) : (
                                                <>
                                                    <button className="ci--edit-pencil-02"
                                                            onClick={() => this.handleEditClick(bedrijf, activeTab)}></button>
                                                </>
                                            )}
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <button className="mdi--trashcan"
                                                    onClick={() => this.handleDelete(bedrijf.gcode, activeTab)}></button>
                                        </td>
                                    </tr>
                                ))}
                                </tbody>
                            </table>
                        </div>
                    )}

                    {activeTab === 'Ervaringdeskundige' && (
                        <div>
                            <h1>Ervaringsdeskundigen</h1>
                            <table>
                                <thead>
                                <tr className="customtablerow">
                                    <th>Gcode</th>
                                    <th>Voornaam</th>
                                    <th>Achternaam</th>
                                    <th>Telefoonnr.</th>
                                    <th>Postcode</th>
                                    <th className="maxcharacters">Straatnaam</th>
                                    <th>Huisnr.</th>
                                    <th>Plaats</th>
                                    <th>Geboortedatum</th>
                                    <th>Email</th>
                                    <th></th>
                                </tr>
                                </thead>
                                <tbody>
                                {ervaringsdeskundigen.map(ervaringsdeskundige => (
                                    <tr className="customtablerow" key={ervaringsdeskundige.gcode}>
                                        <td>{ervaringsdeskundige.gcode}</td>
                                        <td>
                                            {editingRow === ervaringsdeskundige.gcode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="voornaam"
                                                       value={editedData.voornaam || ervaringsdeskundige.voornaam}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                ervaringsdeskundige.voornaam
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === ervaringsdeskundige.gcode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="achternaam"
                                                       value={editedData.achternaam || ervaringsdeskundige.achternaam}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                ervaringsdeskundige.achternaam
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === ervaringsdeskundige.gcode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="telefoonnummer"
                                                       value={editedData.telefoonnummer || ervaringsdeskundige.telefoonnummer}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                ervaringsdeskundige.telefoonnummer
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === ervaringsdeskundige.gcode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="postcode"
                                                       value={editedData.postcode || ervaringsdeskundige.postcode}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                ervaringsdeskundige.postcode
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === ervaringsdeskundige.gcode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="straatnaam"
                                                       value={editedData.straatnaam || ervaringsdeskundige.straatnaam}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                ervaringsdeskundige.straatnaam
                                            )}
                                        </td>
                                        <td className="maxwidth">
                                            {editingRow === ervaringsdeskundige.gcode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="huisnummer"
                                                       value={editedData.huisnummer || ervaringsdeskundige.huisnummer}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                ervaringsdeskundige.huisnummer
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === ervaringsdeskundige.gcode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="plaats"
                                                       value={editedData.plaats || ervaringsdeskundige.plaats}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                ervaringsdeskundige.plaats
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === ervaringsdeskundige.gcode ? (
                                                <input className="wide-input-textfield"
                                                       type="text"
                                                       name="geboortedatum"
                                                       value={editedData.geboortedatum || ervaringsdeskundige.geboortedatum}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                ervaringsdeskundige.geboortedatum
                                            )}
                                        </td>
                                        <td className="maxcharacters">
                                            {editingRow === ervaringsdeskundige.gcode ? (
                                                <input className="wide-input-textfield"
                                                       type="text"
                                                       name="email"
                                                       value={editedData.email || ervaringsdeskundige.email}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                ervaringsdeskundige.email
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === ervaringsdeskundige.gcode ? (
                                                <>
                                                    <button className="uis--check"
                                                            onClick={() => this.handleSaveEdit(editedData, activeTab)}></button>
                                                </>
                                            ) : (
                                                <>
                                                    <button className="ci--edit-pencil-02"
                                                            onClick={() => this.handleEditClick(ervaringsdeskundige, activeTab)}></button>
                                                </>
                                            )}
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <button className="mdi--trashcan"
                                                    onClick={() => this.handleDelete(ervaringsdeskundige.gcode, activeTab)}></button>
                                        </td>
                                    </tr>
                                ))}
                                </tbody>
                            </table>
                        </div>
                    )}
                    {activeTab === 'Beheerder' && (
                        <div>
                            <h1>Beheerders</h1>
                            <table>
                                <thead>
                                <tr className="customtablerow">
                                    <th>Gcode</th>
                                    <th>Voornaam</th>
                                    <th>Achternaam</th>
                                    <th>Rol</th>
                                    <th>Email</th>
                                    <th></th>
                                </tr>
                                </thead>
                                <tbody>
                                {beheerders.map(beheerder => (
                                    <tr className="customtablerow" key={beheerder.gcode}>
                                        <td>{beheerder.gcode}</td>
                                        <td>
                                            {editingRow === beheerder.gcode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="voornaam"
                                                       value={editedData.voornaam || beheerder.voornaam}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                beheerder.voornaam
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === beheerder.gcode ? (
                                                <input className="wide-input-textfield"
                                                       type="text"
                                                       name="achternaam"
                                                       value={editedData.achternaam || beheerder.achternaam}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                beheerder.achternaam
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === beheerder.gcode ? (
                                                <input className="wide-input-textfield"
                                                       type="text"
                                                       name="rol"
                                                       value={editedData.rol || beheerder.rol}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                beheerder.rol
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === beheerder.gcode ? (
                                                <input className="wide-input-textfield"
                                                       type="text"
                                                       name="email"
                                                       value={editedData.email || beheerder.email}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                beheerder.email
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === beheerder.gcode ? (
                                                <>
                                                    <button className="uis--check"
                                                            onClick={() => this.handleSaveEdit(editedData, activeTab)}></button>
                                                </>
                                            ) : (
                                                <>
                                                    <button className="ci--edit-pencil-02"
                                                            onClick={() => this.handleEditClick(beheerder, activeTab)}></button>
                                                </>
                                            )}
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <button className="mdi--trashcan"
                                                    onClick={() => this.handleDelete(beheerder.gcode, activeTab)}></button>
                                        </td>
                                    </tr>
                                ))}
                                </tbody>
                            </table>
                        </div>
                    )}
                    {activeTab === 'Onderzoek' && (
                        <div>
                            <h1>Onderzoeken</h1>
                            <table>
                                <thead>
                                <tr className="customtablerow">
                                    <th>Ocode</th>
                                    <th>Titel</th>
                                    <th>Beschrijving</th>
                                    <th>Locatie</th>
                                    <th>Startdatum</th>
                                    <th>Einddatum</th>
                                    <th>Gezochte Beperking</th>
                                    <th>Gezochte Postcode</th>
                                    <th>Min Leeftijd</th>
                                    <th>Max Leeftijd</th>
                                    <th></th>
                                </tr>
                                </thead>
                                <tbody>
                                {onderzoeken.map(onderzoek => (
                                    <tr className="customtablerow" key={onderzoek.ocode}>
                                        <td>{onderzoek.ocode}</td>
                                        <td className="maxcharacters">
                                            {editingRow === onderzoek.ocode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="titel"
                                                       value={editedData.titel || onderzoek.titel}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                onderzoek.titel
                                            )}
                                        </td>
                                        <td className="maxcharacters">
                                            {editingRow === onderzoek.ocode ? (
                                                <input className="wide-input-textfield"
                                                       type="text"
                                                       name="beschrijving"
                                                       value={editedData.beschrijving || onderzoek.beschrijving}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                onderzoek.beschrijving
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === onderzoek.ocode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="locatie"
                                                       value={editedData.locatie || onderzoek.locatie}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                onderzoek.locatie
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === onderzoek.ocode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="startdatum"
                                                       value={editedData.startdatum || onderzoek.startdatum}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                onderzoek.startdatum
                                            )}
                                        </td>
                                        <td>
                                            {editingRow === onderzoek.ocode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="einddatum"
                                                       value={editedData.einddatum || onderzoek.einddatum}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                onderzoek.einddatum
                                            )}
                                        </td>
                                        <td className="maxwidth">
                                            {editingRow === onderzoek.ocode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="gezochteBeperking"
                                                       value={editedData.gezochteBeperking || onderzoek.gezochteBeperking}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                onderzoek.gezochteBeperking
                                            )}
                                        </td>
                                        <td className="maxwidth">
                                            {editingRow === onderzoek.ocode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="gezochtePostcode"
                                                       value={editedData.gezochtePostcode || onderzoek.gezochtePostcode}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                onderzoek.gezochtePostcode
                                            )}
                                        </td>
                                        <td className="maxwidth">
                                            {editingRow === onderzoek.ocode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="minLeeftijd"
                                                       value={editedData.minLeeftijd || onderzoek.minLeeftijd}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                onderzoek.minLeeftijd
                                            )}
                                        </td>
                                        <td className="maxwidth">
                                            {editingRow === onderzoek.ocode ? (
                                                <input className="slim-input-textfield"
                                                       type="text"
                                                       name="maxLeeftijd"
                                                       value={editedData.maxLeeftijd || onderzoek.maxLeeftijd}
                                                       onChange={this.handleInputChange}
                                                />
                                            ) : (
                                                onderzoek.maxLeeftijd
                                            )}
                                        </td>

                                        <td>
                                            {editingRow === onderzoek.ocode ? (
                                                <>
                                                    <button className="uis--check"
                                                            onClick={() => this.handleSaveEdit(editedData, activeTab)}></button>
                                                </>
                                            ) : (
                                                <>
                                                    <button className="ci--edit-pencil-02"
                                                            onClick={() => this.handleEditClick(onderzoek, activeTab)}></button>
                                                </>
                                            )}
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <button className="mdi--trashcan"
                                                    onClick={() => this.handleDelete(onderzoek.ocode, activeTab)}></button>
                                        </td>
                                    </tr>
                                ))}
                                </tbody>
                            </table>
                        </div>
                    )}
                </div>
            </div>
        );
    }
}

