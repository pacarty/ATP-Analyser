import React from 'react';
import { Line } from 'react-chartjs-2';

import styles from './Displayer.module.css';

class Displayer extends React.Component {  
    render() {
        return (
            <div className={styles.container}>
                <Line
                    data={{
                        labels: this.props.x.map(({date}) => date),
                        datasets: [{
                            data: this.props.x.map(({rankingNumber}) => rankingNumber),
                            label: 'Ranking',
                            borderColor: '#1e871b'
                        }]
                    }}
                />

            </div>
        );
    }
}

export default Displayer;