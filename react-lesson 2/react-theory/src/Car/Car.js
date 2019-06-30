import React from 'react';

export default (props) => (
    <div style={{
        border: '1px solid #ccc',
        marginBottom: '5px',
        display: 'inline-block',
        padding: '10px',
        boxShadow: '0 4px 5px 0 rgba(0, 0, 0, .14)',
        borderRadius: '5px'        
    }}>
        <h3>Car name: {props.name}</h3>
        <p>Year: {props.year}</p>
        {props.children}
        <input type="text" onChange={props.onChangeName} value={props.name} />
        <button onClick={props.delete}>Delete</button>
    </div>
)