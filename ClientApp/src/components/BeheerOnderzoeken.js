import React, { Component } from 'react';
import { Table } from 'reactstrap';
import './BeheerOnderzoeken.css';

export class BeheerOnderzoeken extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: [],
            selectedCell: null,
            editText: '',
            isLoading: true, // Added loading state
        };
    }

    componentDidMount() {
        fetch('https://localhost:7216/api/GetOnderzoeken/GetOnderzoeken')
            .then(response => response.json())
            .then(result => this.setState({ data: result, isLoading: false }))
            .catch(error => {
                console.error('Error fetching data:', error);
                this.setState({ isLoading: false });
            });
    }

    handleDoubleClick = (rowIndex, field) => {
        const selectedCell = { rowIndex, field };
        const initialText = this.state.data[rowIndex][field];
        this.setState({ selectedCell, editText: initialText });
    };

    handleChange = e => {
        this.setState({ editText: e.target.value });
    };

    handleSave = () => {
        const { selectedCell, editText, data } = this.state;

        if (selectedCell) {
            const updatedData = data.map((row, rowIndex) => {
                if (rowIndex === selectedCell.rowIndex) {
                    return {
                        ...row,
                        [selectedCell.field]: editText,
                    };
                }
                return row;
            });

            const selectedIndex = selectedCell.rowIndex;
            
            const putPayload = updatedData.map(row => ({
                titel: row.titel,
                beschrijving: row.beschrijving,
                locatie: row.locatie,
                startdatum: row.startdatum,
                einddatum: row.einddatum,
                gezochteBeperking: row.gezochteBeperking,
                gezochtePostcode: row.gezochtePostcode,
                minLeeftijd: row.minLeeftijd,
                maxLeeftijd: row.maxLeeftijd,
            }));
            
            fetch(`https://localhost:7216/api/Onderzoek/${selectedIndex + 1}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(putPayload[selectedIndex]),
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }
                    return response.json();
                })
                .then(updatedDataFromServer => {
                    console.log('Data updated successfully:', updatedDataFromServer);
                })
                .catch(error => console.error('Error updating data:', error));
            this.setState({ data: updatedData, selectedCell: null, editText: '' });
        }
    };

    handleDelete = (ocode) => {
        const deleteUrl = `https://localhost:7216/api/Onderzoek/${ocode}`;
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
        
        this.setState(prevState => ({
            data: prevState.data.filter(item => item.ocode !== ocode),
        }));
    };

    render() {
        const { data, selectedCell, editText, isLoading } = this.state;

        return (
            <div>
                {isLoading ? (
                    <p>Loading...</p>
                ) : (
                    <>
                        <Table size="sm" striped bordered hover className="onderzoeken-table">
                            <thead>
                            <tr>
                                <th>#</th>
                                <th>Titel</th>
                                <th>Beschrijving</th>
                                <th>Locatie</th>
                                <th>Startdatum</th>
                                <th>Einddatum</th>
                                <th>Gezochte Beperking</th>
                                <th>Gezochte Postcode</th>
                                <th>Minimale Leeftijd</th>
                                <th>Maximale Leeftijd</th>
                            </tr>
                            </thead>
                            <tbody>
                            {data.map((item, rowIndex) => (
                                <tr key={rowIndex}>
                                    {Object.keys(item).map((field, colIndex) => (
                                        <td
                                            key={colIndex}
                                            onDoubleClick={() => this.handleDoubleClick(rowIndex, field)}
                                        >
                                            {item[field]}
                                        </td>
                                    ))}
                                    <td>
                                        <button className="custom-button" onClick={() => this.handleDelete(item.ocode)}>
                                            Delete
                                        </button>
                                    </td>
                                </tr>
                            ))}
                            </tbody>
                        </Table>

                        {selectedCell && (
                            <div className="onderzoek-verander-container">
                                <textarea value={editText} onChange={this.handleChange} />
                                <button onClick={this.handleSave}>Save</button>
                            </div>
                        )}
                    </>
                )}
            </div>
        );
    }
}