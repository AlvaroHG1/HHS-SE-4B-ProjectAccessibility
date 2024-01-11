import React, { Component } from 'react';
import './Chats.css';
import {render} from "react-dom";

export class Chats extends Component {

    constructor(props) {
        super(props);
        this.state = {chats: [], loading: true};
    }

    async populateChatData() {
        try {
            const response = await fetch('api/Chat', {
                method: 'GET',
                headers:
                    {
                        'Accept':
                            'application/json',
                        'Content-Type':
                            'application/json'
                    }
            });
            console.log('Fetch response:', response);
            if (!response.ok) {
                console.error(`Error: ${response.status} - ${response.statusText}`);
                return;
            }
            const chatData = await response.json();
            this.setState({chats: chatData, loading: false});
            console.log('chats:', chatData);
        } catch (error) {
            console.error('Exception during fetch:', error);
        }
    }


        render()
        {
            const {chats, loading} = this.state;
            console.log('chats:', chats);
            if (loading) {
                return <div>Loading...</div>;
            }
            return (
                <div className="achtergrond">
                    <div className="chat-container">
                        <div className="chat-list">
                            <div className="chat-item">
                                <h2>{`Naam: ${chats.chats.naam}`}</h2>
                                <p>Chat 1 description</p>
                            </div>
                            <div className="chat-item">
                                <h2>Chat 2</h2>
                                <p>Chat 2 description</p>
                            </div>
                            <div className="chat-item">
                                <h2>Chat 3</h2>
                                <p>Chat 3 description</p>
                            </div>
                            <div className="chat-item">
                                <h2>Chat 4</h2>
                                <p>Chat 4 description</p>
                            </div>
                            <div className="chat-item">
                                <h2>Chat 5</h2>
                                <p>Chat 5 description</p>
                            </div>
                            <div className="chat-item">
                                <h2>Chat 6</h2>
                                <p>Chat 6 description</p>
                            </div>
                            <div className="chat-item">
                                <h2>Chat 7</h2>
                                <p>Chat 7 description</p>
                            </div>
                            <div className="chat-item">
                                <h2>Chat 8</h2>
                                <p>Chat 8 description</p>
                            </div>
                            <div className="chat-item">
                                <h2>Chat 9</h2>
                                <p>Chat 9 description</p>
                            </div>
                            <div className="chat-item">
                                <h2>Chat 10</h2>
                                <p>Chat 10 description</p>
                            </div>
                        </div>
                    </div>
                </div>

            );

        }

    }

