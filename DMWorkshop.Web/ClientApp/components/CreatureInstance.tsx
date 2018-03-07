import * as React from 'react';
import { Image, Icon, Table, Progress } from "semantic-ui-react";
import { Slider } from 'react-semantic-ui-range';
import { NumberPicker } from 'semantic-ui-react-numberpicker';
import * as Encounters from '../store/Encounters';

type CreatureInstanceProps =
    {
        instance: Encounters.CreatureInstance,
        changeCreatureHp: (instance: Encounters.CreatureInstance, newHp: number) => Encounters.ChangeCreatureHpAction
    }

export class CreatureInstance extends React.Component<CreatureInstanceProps, CreatureInstanceProps> {
    public render() {
        return (
            <Table.Row>
                <Table.Cell collapsing><Icon name='lightning' /> {this.props.instance.initiative}</Table.Cell>
                <Table.Cell collapsing><Image size='mini' src={'/api/creatures/' + this.props.instance.creature.name + '/image'} /></Table.Cell>
                <Table.Cell collapsing>{this.props.instance.creature.name}</Table.Cell>
                <Table.Cell>
                    <Icon name='plus' /> <b className='large text'>{this.props.instance.hp}</b>{'/' + this.props.instance.creature.maxHP}
                    <Progress total={this.props.instance.creature.maxHP} value={this.props.instance.hp} indicating attached='bottom' />
                </Table.Cell>
                <Table.Cell collapsing><Icon name='lightning' /> {this.props.instance.initiative}</Table.Cell>
            </Table.Row>
            );
    }
}