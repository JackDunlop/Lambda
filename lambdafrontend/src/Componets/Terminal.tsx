﻿import "../Styling/Terminal.css"
import { useState } from 'react';

const API_BASE_URL = 'http://localhost:5074';  

function Terminal() {
    const [inputValue, setInputValue] = useState("");
    const [error, setError] = useState<string>('');

    const lambdaASTFetch = async () => {
        try {
            const response = await fetch(`${API_BASE_URL}/Lambda?input=${inputValue}`);  
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }  
            const data = await response.json();
            if (data.error == false) {
                setInputValue(data.data);
                console.log(inputValue);
            }
        }
        catch (error: unknown) {
            setError(`An unexpected error occurred: ${error}`);  
        }
    }

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        console.log(inputValue);
        const test = lambdaASTFetch();
        console.log(test);
        setInputValue("");
    };

    return (
        <div className="card">
            <div className="wrap">
                <div className="terminal">
                    <hgroup className="head">
                        <p className="title">
                            <svg width="16px"
                                height="16px"
                                aria-hidden="true"
                                xmlns="http://www.w3.org/2000/svg"
                                viewBox="0 0 24 24"
                                stroke-linejoin="round"
                                stroke-linecap="round"
                                stroke-width="2"
                                stroke="currentColor"
                                fill="none">
                                <path d="M7 15L10 12L7 9M13 15H17M7.8 21H16.2C17.8802 21 18.7202 21 19.362 20.673C19.9265 20.3854 20.3854 19.9265 20.673 19.362C21 18.7202 21 17.8802 21 16.2V7.8C21 6.11984 21 5.27976 20.673 4.63803C20.3854 4.07354 19.9265 3.6146 19.362 3.32698C18.7202 3 17.8802 3 16.2 3H7.8C6.11984 3 5.27976 3 4.63803 3.32698C4.07354 3.6146 3.6146 4.07354 3.32698 4.63803C3 5.27976 3 6.11984 3 7.8V16.2C3 17.8802 3 18.7202 3.32698 19.362C3.6146 19.9265 4.07354 20.3854 4.63803 20.673C5.27976 21 6.11984 21 7.8 21Z"></path>
                            </svg>
                            Terminal
                        </p>
                    </hgroup>

                    <div className="body">
                        <pre className="pre">
                            <code>λ&nbsp;</code>
                            <code>{">"}&nbsp;</code>
                            <form onSubmit={handleSubmit}>
                                <input 
                                    type="text" 
                                    className="cmd" 
                                    value={inputValue} 
                                    onChange={(e) => setInputValue(e.target.value)} 
                                />
                            </form>
                        </pre>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Terminal;