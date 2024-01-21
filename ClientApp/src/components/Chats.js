import React, {Component} from 'react';
import './Chats.css';

export class Chats extends Component {
    constructor(props) {
        super(props);
        this.state = {
            OpenChats: [],
            selectedChat: null,
            chatMessages: [],
            newMessage: '',
            loading: true,
        };
        this.chatInfoChatsContainerRef = React.createRef();
    }

    async componentDidMount() {
        try {
            const responseChats = await fetch('https://localhost:7216/api/OpenChat/2', {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
            });

            if (!responseChats.ok) {
                return;
            }

            const OpenChats = await responseChats.json();

            await this.handleChatClick(OpenChats[0]);

            this.setState({
                OpenChats,
                loading: false,
            });
        } catch (error) {
            console.error('Exception during fetch:', error);
        }
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevState.chatMessages !== this.state.chatMessages) {
            this.scrollToBottom();
        }
    }

    scrollToBottom = () => {
        if (this.chatInfoChatsContainerRef.current) {
            this.chatInfoChatsContainerRef.current.scrollTop = this.chatInfoChatsContainerRef.current.scrollHeight;
        }
    };

    handleChatClick = async (chat) => {
        this.setState({loading: true});

        try {
            const {senderGCode, receiverGCode} = chat;

            const responseMessages = await fetch(
                `https://localhost:7216/api/Chat/GetChatsByUsers/?senderCode=${senderGCode}&receiverCode=${receiverGCode}`
            );

            if (!responseMessages.ok) {
                console.error(`Failed to fetch messages: ${responseMessages.statusText}`);
            }

            const messages = await responseMessages.json();

            this.setState({
                selectedChat: chat,
                chatMessages: messages,
                loading: false,
                error: null,
            });
        } catch (error) {
            console.error('Error fetching messages:', error);
            this.setState({
                loading: false,
                error: 'Failed to fetch messages. Please try again.',
            });
        }
    };

    handleKeyDown = (e) => {
        if (e.key === 'Enter') {
            e.preventDefault();
            this.sendMessage();
        }
    };

    sendMessage = async () => {
        const { selectedChat, newMessage, chatMessages } = this.state;

        try {
            const response = await fetch('https://localhost:7216/api/Chat', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    senderGCode: 2,
                    recieverGCode: selectedChat.receiverGCode,
                    message: newMessage,
                    dateTime: new Date().toISOString(),
                }),
            });

            if (!response.ok) {
                console.error(`Failed to send message: ${response.statusText}`);
                return;
            }
            
            const newChatMessages = [
                {
                    senderName: 'Alvaro',
                    message: newMessage,
                    dateTime: new Date().toISOString(),
                },
                ...chatMessages,
            ];

            this.setState({
                chatMessages: newChatMessages,
            }, () => {
                if (this.chatInfoChatsContainerRef.current) {
                    this.chatInfoChatsContainerRef.current.scrollTop = this.chatInfoChatsContainerRef.current.scrollHeight;
                }
            });

            this.setState({ newMessage: '', chatMessages: newChatMessages });
        } catch (error) {
            console.error('Error sending message:', error);
        }
    };
    
    

    render() {
        const {selectedChat, loading, OpenChats, chatMessages, newMessage} = this.state;

        return (
            <div className="total-container">
                <div className="chat-container">
                    <div className="chat-container">
                        <div className="chat-list">
                            {OpenChats.map((chat) => (
                                <button
                                    key={chat.oCcode}
                                    className={`chat-item ${selectedChat === chat ? 'selected' : ''}`}
                                    onClick={() => this.handleChatClick(chat)}
                                >
                                    <h5>{`${chat.naam}`}</h5>
                                    <p>{`${chat.type}`}</p>
                                </button>
                            ))}
                        </div>
                    </div>
                </div>
                <div className="chat-info">
                    <div className="chat-info-chats-container" ref={this.chatInfoChatsContainerRef}>
                        {selectedChat && (
                            <div className="message-list">
                                {chatMessages.sort((a, b) => new Date(a.dateTime) - new Date(b.dateTime)).map((message) => (
                                    <div className="message-box" key={message.id}>
                                        <div>
                                            <p className="senderName">{message.senderName}</p>
                                            <p className="message-text">{message.message}</p>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        )}
                    </div>
                    <div className="message-input">
                        <input
                            className="sendMessage"
                            type="text"
                            placeholder="Stuur een bericht..."
                            value={newMessage}
                            onChange={(e) => this.setState({ newMessage: e.target.value })}
                            onKeyDown={this.handleKeyDown}
                        />
                        <button className="sendButton" onClick={this.sendMessage}>Verstuur</button>
                    </div>
                </div>
            </div>
        );
    }
}

export default Chats;