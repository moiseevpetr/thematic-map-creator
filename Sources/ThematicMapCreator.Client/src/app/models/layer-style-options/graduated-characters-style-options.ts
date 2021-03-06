import { LayerStyleOptions } from './layer-style-options';
import { LayerStyle } from './layer-style.enum';
import { DependencyType } from '../../core/dependency.type';

export class GraduatedCharactersStyleOptions implements LayerStyleOptions {
    style: LayerStyle.GraduatedCharacters;
    minSize = 6;
    maxSize = 10;
    propertyName: string;
    color = '#000000';
    fillColor = '#000000';
    minValue: number;
    maxValue: number;
    dependency: DependencyType = DependencyType.Linear;
}
