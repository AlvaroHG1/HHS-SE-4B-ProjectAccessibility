import React, { useState, useEffect, useRef } from 'react';
import './OnderzoekAanmaken.css';
import "react-datepicker/dist/react-datepicker.css";


export function OnderzoekAanmaken() {
    const initialState = {
        setLoading: true,
    };

    const [state, setState] = useState(initialState);
    const beperkingenRef = useRef([]);

    const handleChange = event => {
        const { name, value } = event.target;

        setState(prevState => ({
            ...prevState,
            [name]: value
        }));
    }

    const startOnderzoek = async () => {
        const { titel,
            beschrijving,
            locatie,
            startdatum,
            einddatum,
            gezochteBeperking,
            gezochtePostcode,
            minLeeftijd,
            maxLeeftijd} = state;
        console.log('state:', state);
        let response1;
        try {
            response1 = await fetch('https://localhost:7216/api/Onderzoek', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Titel: titel,
                    Beschrijving: beschrijving,
                    Locatie: locatie,
                    Startdatum: startdatum,
                    Einddatum: einddatum,
                    GezochteBeperking: 'Kleurenblind',
                    GezochtePostcode: gezochtePostcode,
                    MinLeeftijd: minLeeftijd,
                    MaxLeeftijd: maxLeeftijd
                })
            });
            // console.log(await response1.json());
            if (!response1.ok) {
                console.error(`Error: ${response1.status} - ${response1.statusText}`);
                return;
            }
            const responseData = await response1.json();
            console.log(responseData);

            // Extract oCode from responseData and use it as needed
            const oCode = responseData.ocode;
            console.log('oCode:', oCode);
            const response = await fetch('https://localhost:7216/api/HeeftOnderzoek', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    oCode: oCode,
                    bCode: 3
                })
            });
            //redirect to bedrijvenonderzoek page
            // console.log(await response.json());
            if (!response.ok) {
                console.error(`Error: ${response.status} - ${response.statusText}`);
                return;
            }
            

            setState(initialState);
        } catch (error) {
            console.error('Exception during fetch:', error);
        }
    }

    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchBeperkingen = async () => {
            try {
                const response = await fetch('https://localhost:7216/api/Beperking/Beperkingen', {
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

                const beperkingen = await response.json();
                console.log(beperkingen);
                beperkingenRef.current = beperkingen;
            } catch (error) {
                console.error('Exception during fetch:', error);
            }
            setLoading(false);
        };

        fetchBeperkingen();
    }, []);

    const {
        titel,
        beschrijving,
        locatie,
        startdatum,
        einddatum,
        gezochteBeperking,
        gezochtePostcode,
        minLeeftijd,
        maxLeeftijd
    } = state;

    if (loading) {
        return <div>Loading...</div>;
    }
    return (
        <div style={{padding: 20}}>
            <form>
                <label>Titel</label>
                <input
                    type="text"
                    name="titel"
                    value={titel}
                    onChange={handleChange}/>
                <label>Beschrijving</label>
                <input
                    type="text"
                    name="beschrijving"
                    value={beschrijving}
                    onChange={handleChange}/>
                <label>Locatie</label>
                <input
                    type="text"
                    name="locatie"
                    value={locatie}
                    onChange={handleChange}/>
                <label>Startdatum</label>
                <input
                    type="date"
                    name="startdatum"
                    value={startdatum}
                    onChange={handleChange}
                />
                <label>Einddatum</label>
                <input
                    type="date"
                    name="einddatum"
                    value={einddatum}
                    onChange={handleChange}
                />
                <label>GezochteBeperking</label>
                <select name="gezochteBeperking" value={gezochteBeperking} onChange={handleChange}>
                    {beperkingenRef.current.map(beperking => (
                        <option key={beperking.id} value={beperking.id}>{beperking.naam}</option>
                    ))}
                </select>
                <label>GezochtePostcode</label>
                <input
                    type="text"
                    name="gezochtePostcode"
                    value={gezochtePostcode}
                    onChange={handleChange}/>
                <label>MinLeeftijd</label>
                <input
                    type="text"
                    name="minLeeftijd"
                    value={minLeeftijd}
                    onChange={handleChange}/>
                <label>MaxLeeftijd</label>
                <input
                    type="text"
                    name="maxLeeftijd"
                    value={maxLeeftijd}
                    onChange={handleChange}/>
            </form>
            <button onClick={startOnderzoek}>Start Onderzoek</button>
        </div>
    );

}