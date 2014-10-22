//
//  PlaceImageCell.m
//  CityPlace
//
//  Created by Yuri Korshev on 22.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "PlaceImageCell.h"
#import <QuartzCore/QuartzCore.h>

@implementation PlaceImageCell

@synthesize placeImage;

- (void)awakeFromNib {
    placeImage.layer.backgroundColor=[[UIColor clearColor] CGColor];
    placeImage.layer.cornerRadius=150.0f;
    placeImage.layer.borderWidth=1.0;
    placeImage.layer.masksToBounds = YES;
    placeImage.layer.borderColor=[[UIColor whiteColor] CGColor];
}

- (void)setSelected:(BOOL)selected animated:(BOOL)animated {
    [super setSelected:selected animated:animated];

    // Configure the view for the selected state
}

@end
