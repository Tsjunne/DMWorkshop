import * as React from 'react';
import { Image, Icon, Table, Progress, Button, Popup } from "semantic-ui-react";
import { Slider } from 'react-semantic-ui-range';
import { NumberPicker } from 'semantic-ui-react-numberpicker';
import * as Encounters from '../store/Encounters';
import * as Model from '../model/CreatureInstance';

type CreatureInstanceProps =
    {
        instance: Model.CreatureInstance,
        changeCreatureHp: (instance: Model.CreatureInstance, newHp: number) => Encounters.ChangeCreatureHpAction
        changeCreatureCondition: (instance: Model.CreatureInstance, condition: Model.Condition, add: boolean) => Encounters.ChangeCreatureConditionAction
    }

export class CreatureInstance extends React.Component<CreatureInstanceProps, CreatureInstanceProps> {
    public render() {
        return (
            <Table.Row >
                <Table.Cell collapsing><Icon name='lightning' /> {this.props.instance.initiative}</Table.Cell>
                <Table.Cell collapsing><Image size='mini' src={'/api/creatures/' + this.props.instance.creature.name + '/image'} />
                    <Progress total={this.props.instance.creature.maxHP} value={this.props.instance.hp} indicating attached='bottom' /></Table.Cell>
                <Table.Cell collapsing>{this.props.instance.creature.name}</Table.Cell>
                <Table.Cell collapsing>
                    <Icon name='shield' /> <b className='large text'>{this.props.instance.creature.ac}</b>
                </Table.Cell>
                <Table.Cell collapsing>
                    <Icon name='plus' /> <b className='large text'>{this.props.instance.hp}</b>{'/' + this.props.instance.creature.maxHP}
                </Table.Cell>
                <Table.Cell>
                    {this.props.instance.conditions.map(condition =>
                        <Button size='mini' compact basic onClick={() => this.props.changeCreatureCondition(this.props.instance, condition, false)}>
                            <Image avatar src={'/images/' + condition.toString() + '.svg'} />
                        </Button>
                    )}
                    <Slider color={this.determineColor(this.props.instance)} settings={{
                        start: this.props.instance.hp,
                        min: 0,
                        max: this.props.instance.creature.maxHP,
                        step: 1,
                        onChange: (value: number) => this.props.changeCreatureHp(this.props.instance, value)
                    }
                    } />
                </Table.Cell>
                <Table.Cell collapsing>
                    <Popup position='bottom right' wide='very'
                        trigger={<Button icon='plus'/>}
                        content={
                            <Button.Group compact>
                                <Button circular content={<Image avatar src='/images/charmed.svg' />} onClick={() => this.props.changeCreatureCondition(this.props.instance, Model.Condition.Charmed, true)}/>
                                <Button circular content={<Image avatar src='/images/stunned.svg' />} onClick={() => this.props.changeCreatureCondition(this.props.instance, Model.Condition.Stunned, true)}/>
                                <Button circular content={<Image avatar src='/images/poisoned.svg' />} onClick={() => this.props.changeCreatureCondition(this.props.instance, Model.Condition.Poisoned, true)}/>
                                <Button circular content={<Image avatar src='/images/blinded.svg' />} onClick={() => this.props.changeCreatureCondition(this.props.instance, Model.Condition.Blinded, true)}/>
                            </Button.Group>
                        }
                        on='click'
                        hideOnScroll
                    />
                </Table.Cell>
            </Table.Row>
        );
    }

    determineColor(instance: Model.CreatureInstance): string {
        var percent = instance.hp / instance.creature.maxHP;
        if (percent > 0.8) return "green";
        if (percent > 0.6) return "olive";
        if (percent > 0.4) return "yellow";
        if (percent > 0.2) return "orange";
        return "red";
    }
}