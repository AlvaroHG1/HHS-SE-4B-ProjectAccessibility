import React, { Component } from 'react';
import './Chats.css';

export class Chats extends Component {
    constructor(props) {
        super(props);
        this.state = { OpenChat: [], loading: true };
    }

    async componentDidMount() {
        try {
            const response = await fetch('https://localhost:7216/api/OpenChat/2', {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });

            console.log('Fetch response:', response);

            if (!response.ok) {
                console.error(`Error: ${response.status} - ${response.statusText}`);
                return;
            }

            const OpenChatData = await response.json();
            this.setState({ OpenChat: OpenChatData, loading: false });
            console.log('chats:', OpenChatData);
        } catch (error) {
            console.error('Exception during fetch:', error);
        }
    }

    render() {
        const { OpenChat, loading } = this.state;
        console.log('chats:', OpenChat);

        if (loading) {
            return <div>Loading...</div>;
        }

        return (
            <div className="achtergrond">
                <div className="chat-container">
                    <div className="chat-list">
                        {OpenChat.map((chat) => (
                            <div key={chat.oCcode} className="chat-item">
                                <h2>{`Naam: ${chat.naam}`}</h2>
                                <p>{`Description: ${chat.type}`}</p>
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        );
    }
}

