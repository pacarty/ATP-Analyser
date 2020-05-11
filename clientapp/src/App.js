import React from 'react';

import { PlayerSelector } from './components';
import styles from './App.module.css';

class App extends React.Component {
    render() {
        return (
            <div className={styles.container}>
                <h2>ATP rankings analyser</h2>
                <PlayerSelector />
            </div>
        );
    }
}

export default App;